import { Component, OnInit, PipeTransform } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { UserService } from 'src/app/core/services';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { UserViewModel } from 'src/app/shared/models';
import { RoleViewModel } from 'src/app/shared/models/role.model';
import { FormControl } from '@angular/forms';
import { Observable, pipe } from 'rxjs';
import { startWith, map } from 'rxjs/internal/operators';
import { faEdit } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})

export class UsersComponent extends BaseComponent implements OnInit {
  faEdit = faEdit;
  users: UserViewModel[] = [];
  users$: Observable<UserViewModel[]>;
  filter = new FormControl('');
  constructor(private readonly userService: UserService) {
    super();
    this.users$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => this.search(text))
    );
  }

  ngOnInit() {
    this.getUsers();
  }
  search(text: string): UserViewModel[] {
    return this.users.filter(user => {
      const term = text.toLowerCase();
      return user.userName.toLowerCase().includes(term)
          || user.email.toLowerCase().includes(term);
          // || user.roles.some(x => x.toLowerCase().includes(term));
    });
  }
  getUsers() {
    this.getAllUsers();
  }

  bindRoles(roles: string[]) {
    return roles.join(', ');
  }

  getAllUsers() {
    this.subscriptions.push(
      this.userService
        .getAllUsers()
        .subscribe((response: ResponseModel<UserViewModel[]>) => {
          this.users = response.payload;
        })
    );
  }

}
