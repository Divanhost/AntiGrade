import { Injectable } from '@angular/core';
import * as jwtDecode from 'jwt-decode';
import { Router } from '@angular/router';
import { CurrentUserViewModel } from 'src/app/shared/models/current-user.model';


@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor() { }

  getCurrentUser() {
    const token = localStorage.getItem('jwtToken');
    const decodedToken = jwtDecode(token);
    const userId = decodedToken.nameid;
    const username = decodedToken.sub;
    const currentUser: CurrentUserViewModel = {
      id: parseInt(userId, 10),
      userName: username,
    };
    return currentUser;
  }
  getCurrentUserEmployeeId() {
    const token = localStorage.getItem('jwtToken');
    const decodedToken = jwtDecode(token);
    const employeeId = decodedToken.employeeId;
    return employeeId;
  }

  checkIfRole(role: string) {
    const token = localStorage.getItem('jwtToken');
    const decodedToken = jwtDecode(token);
    const roles = decodedToken.roles;

    if (typeof roles === 'string') {
      return roles === role;
    } else if (typeof roles === 'object') {
      return roles.find(x => x === role) ? true : false;
    } else {
      return false;
    }
  }
}
