import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs';
import { UserDtoModel } from 'src/app/shared/models/user-dto.model';
import { UserViewModel } from 'src/app/shared/models';
import { RoleViewModel } from 'src/app/shared/models/role.model';
import { RoleService } from 'src/app/core/services/role.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { PagedResponseModel } from 'src/app/shared/models/paged-response.model';
import { UserService } from 'src/app/core/services';
import { Role } from 'src/app/shared/enums/role.enum';


@Component({
  selector: 'app-user-edit-create',
  templateUrl: './user-edit-create.component.html',
  styleUrls: ['./user-edit-create.component.scss']
})
export class UserEditCreateComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  private readonly notifier: NotifierService;
  addUserForm: FormGroup;
  submitted = false;
  isCreateMode = true;
  isShowDeleteButton: boolean;
  isShowChangePassword: boolean;
  showOldPasswordInput: boolean;
  showPasswordInput: boolean;
  userNameInDb: string;
  emailInDb: string;
  gitlabNameInDb: string;
  isDeleteMode = false;
  isCreateUserSuccessfully = false;
  checked: boolean;
  mustMatch: boolean;
  showPasswords: boolean;
  userId: number;
  accountId: number;
  user: UserDtoModel = new UserDtoModel();
  editUser: UserDtoModel = new UserDtoModel();
  currentUserId: number;
  pageTitle = 'Create user';
  roles = [];
  isPasswordCorrect = true;
  isShowAvatarBlock = true;
  passwordValidationPattern = '((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,20})';

  constructor(private location: Location,
              private readonly userService: UserService,
              private readonly roleService: RoleService,
              private readonly route: ActivatedRoute,
              private router: Router,
              notifierService: NotifierService) {
    this.currentUserId = roleService.getCurrentUser().id;
    this.subscriptions.push(
      this.route.params.subscribe(params => this.userId = params.id)
    );
    this.notifier = notifierService;
    this.isCreateMode = this.userId === undefined;
    this.isShowDeleteButton = this.userId !== undefined && this.isAdminRole;
  }

  ngOnInit() {

    if (this.isCreateMode) {
      this.showPasswordInput = true;
    } else {
      // this.getUserByID();
      this.isShowChangePassword = true;
    }
    this.initForm();
    this.getAllRoles();
  }

  initForm() {
    this.submitted = false;
    this.addUserForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.pattern(/^\S*$/)]),
      email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(50)]),
      roles: new FormControl('', Validators.required),
      oldPassword: new FormControl(''),
      newPassword: new FormControl(''),
      confirmPassword: new FormControl(''),
    });
    if (this.isCreateMode) {
      this.addUserForm.controls.newPassword.setValidators([Validators.required, Validators.pattern(this.passwordValidationPattern)]);
      this.addUserForm.controls.newPassword.updateValueAndValidity();
    }
  }

  fillForm() {
    this.addUserForm.controls.userName.setValue(this.user.userName);
    this.addUserForm.controls.email.setValue(this.user.email);
    this.addUserForm.controls.roles.setValue(this.user.roles);
  }

  getUserByID() {
    this.subscriptions.push(
      this.userService
        .getUserByID(this.userId)
        .subscribe((response: ResponseModel<UserDtoModel>) => {
          this.user = response.payload;
          this.user.roles = this.roles.filter(x => response.payload.roles.some(y => y === x));
          this.pageTitle = `Edit ${this.user.userName}`;
          this.fillForm();
        })
    );
  }

  getEditUserById() {
    this.subscriptions.push(
      this.userService
        .getUserByID(this.userId)
        .subscribe((response: ResponseModel<UserDtoModel>) => {
          this.editUser = response.payload;
        })
    );
  }

  getAllRoles() {
    this.subscriptions.push(
      this.userService
        .getAllRoles()
        .subscribe((response: PagedResponseModel<RoleViewModel>) => {
          this.roles = response.payload.map(x => x.name);
          if (!this.isCreateMode) {
            this.getUserByID();
          }
        })
    );
  }

  isInputHasErrors(input) {
    return this.submitted && input.errors;
  }

  get form() { return this.addUserForm.controls; }

  checkUserName() {
    this.subscriptions.push(
      this.userService
        .checkUserNameExists(this.user.userName)
        .subscribe((response: ResponseModel<string>) => {
          this.userNameInDb = response.payload;
        })
    );
  }

  checkEmail() {
    this.subscriptions.push(
      this.userService
        .checkEmailExists(this.user.email)
        .subscribe((response: ResponseModel<string>) => {
          this.emailInDb = response.payload;
        })
    );
  }

  checkPassword() {
    this.subscriptions.push(
      this.userService
        .checkPassword(this.user.userName, this.user.oldPassword)
        .subscribe((response: ResponseModel<boolean>) => {
          this.isPasswordCorrect = response.payload;
        })
    );
  }


  changePassword(event) {
    if (event.target.checked) {
      this.showPasswords = true;
      this.showPasswordInput = true;
      this.showOldPasswordInput = true;
      this.addUserForm.controls.newPassword.setValidators([Validators.required, Validators.pattern(this.passwordValidationPattern)]);
      if (/*this.isAdminRole &&*/ (this.userId !== this.currentUserId)) {
        this.showOldPasswordInput = false;
      } else {
        this.addUserForm.controls.oldPassword.setValidators([Validators.required, Validators.pattern(this.passwordValidationPattern)]);
      }
    } else {
      this.showPasswords = false;
      this.showPasswordInput = false;
      this.showOldPasswordInput = false;
      this.addUserForm.controls.oldPassword.clearValidators();
      this.addUserForm.controls.newPassword.clearValidators();
    }
    this.addUserForm.controls.oldPassword.updateValueAndValidity();
    this.addUserForm.controls.newPassword.updateValueAndValidity();
  }

  createOrUpdateUser() {
    this.checkUserName();
    this.checkEmail();
    if (this.showPasswords && !this.isAdminRole) {
      this.checkPassword();
    }
    if (this.isCreateMode) {
      this.subscriptions.push(
        this.userService
          .createUser(this.user)
          .subscribe((response: ResponseModel<UserDtoModel>) => {
            if (response.payload !== null) {
              this.userId = response.payload.id;
              this.notifier.notify('success', 'User created successfully!');
              this.router.navigate(['/users']);
            } else {
              this.notifier.notify('error', 'Whoops, something went wrong!');
            }
          })
      );
    } else {
      this.getEditUserById();
      this.subscriptions.push(
        this.userService
          .updateUser(this.userId, this.user)
          .subscribe((response: ResponseModel<UserDtoModel>) => {
            if (response.payload !== null) {
              if (this.user.userName !== this.userNameInDb || this.user.userName === this.editUser.userName) {
                if (this.user.email !== this.emailInDb || this.user.email === this.editUser.email) {
                  if (this.isPasswordCorrect) {
                    this.notifier.notify('success', 'User updated successfully!');
                    this.router.navigate(['/users']);
                  } else {
                    this.notifier.notify('error', 'This old password incorrect!');
                  }
                } else {
                  this.notifier.notify('error', 'This email already exists!');
                }
              } else {
                this.notifier.notify('error', 'This username already exists!');
              }
            } else {
              this.notifier.notify('error', 'Whoops, something went wrong!');
            }
          })
      );
    }
  }

  deleteUser() {
    this.subscriptions.push(
      this.userService
        .deleteUser(this.userId)
        .subscribe((response: ResponseModel<UserDtoModel>) => {
          if (response.payload) {
            this.notifier.notify('success', 'User deleted successfully!');
            this.router.navigate(['/users']);
          } else {
            this.isDeleteMode = false;
            this.notifier.notify('error', 'Whoops, something went wrong!');
          }
        })
    );
  }

  onSubmit() {
    this.mustMatch = false;
    if (this.isDeleteMode) {
      this.deleteUser();
    } else {
      this.submitted = true;

      if (this.addUserForm.invalid) {
        return;
      }
      this.user = this.addUserForm.getRawValue();
      this.user.id = this.userId;

      if (!this.isCreateMode && this.user.newPassword !== this.user.confirmPassword) {
        this.mustMatch = true;
        return;
      }
      this.createOrUpdateUser();
    }
  }

  get isAdminRole(): boolean {
    return this.roleService.checkIfRole(Role.Admin);
  }

  get isShowFinanceTab(): boolean {
    return this.isAdminRole && !this.isCreateMode && this.userId !== this.currentUserId;
  }

  get isNotCurrentAdminProfile(): boolean {
    return this.isAdminRole && this.currentUserId !== this.userId;
  }

  ngOnDestroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }
}
