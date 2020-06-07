import { Component, OnInit } from '@angular/core';
import { CurrentUserViewModel } from 'src/app/shared/models/current-user.model';
import { RoleService } from 'src/app/core/services/role.service';
import { LoginService } from 'src/app/core/services';
import { Router } from '@angular/router';
import { Role } from 'src/app/shared/enums/role.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  currentUser: CurrentUserViewModel = new CurrentUserViewModel();
  constructor(private readonly roleService: RoleService,
              private readonly loginService: LoginService,
              private readonly router: Router) {
    this.getCurrentUser();
  }

  ngOnInit(): void {
  }
  getCurrentUser() {
    this.currentUser = this.roleService.getCurrentUser();
    localStorage.setItem('currentId', this.currentUser.id.toString());
  }
  get isAdmin() {
    return this.roleService.checkIfRole(Role.Admin);
  }
  logout() {
    this.loginService.logout();
    this.router.navigate(['/login']);
  }
}
