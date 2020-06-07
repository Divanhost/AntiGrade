import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { RoleService } from '../services/role.service';
import { Role } from 'src/app/shared/enums/role.enum';

@Injectable({
  providedIn: 'root'
})
export class AdminRoleGuard implements CanActivate {

  constructor(private router: Router,
              private roleService: RoleService) { }

  canActivate() {
    if (this.roleService.checkIfRole(Role.Admin)) {
      return true;
    }

    this.router.navigate(['/dashboard']);
    return false;
  }
}
