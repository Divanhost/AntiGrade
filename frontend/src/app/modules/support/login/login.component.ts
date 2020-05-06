import { Component, OnInit } from '@angular/core';
import { LoginModel } from 'src/app/shared/models/login.model';
import { Subscription } from 'rxjs';
import { UserViewModel } from 'src/app/shared/models/user-view.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseFormComponent } from 'src/app/shared/classes';
import { LoginService, HttpService } from 'src/app/core/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [
    HttpService
  ]
})
export class LoginComponent extends BaseFormComponent implements OnInit {

  subscriptions: Subscription[] = [];
  model: LoginModel;
  currentUser: UserViewModel;
  submitted = false;
  loading = false;

  constructor(private readonly loginService: LoginService,
    private readonly router: Router) {
      super();
  }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.model = new LoginModel();
    this.submitted = false;
    this.form = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      isRemembered: new FormControl('')
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.model.userName = this.form.value.userName;
    this.model.password = this.form.value.password;
    this.subscriptions.push(
      this.loginService.login(this.model)
        .subscribe(response => {
          this.loginService.storeToken(response);
          const url = localStorage.getItem('lastUrl');
          if (url) {
            localStorage.removeItem('lastUrl');
            this.router.navigate([url]);
          } else {
            this.router.navigate(['/dashboard']);
          }
        })
    );
  }

  ngOnDestroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

}
