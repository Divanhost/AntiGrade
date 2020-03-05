import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { RoleService } from '@bi/services';
import { Role } from '@bi/enums';

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
