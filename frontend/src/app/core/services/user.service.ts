import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { PagedResponseModel } from 'src/app/shared/models/paged-response.model';
import { UserViewModel } from 'src/app/shared/models';
import { RoleViewModel } from 'src/app/shared/models/role.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { UserDtoModel } from 'src/app/shared/models/user-dto.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpService) {

  }

  getAllUsers(): Observable<PagedResponseModel<UserViewModel>> {
    return this.http.getData(`users/get`);
  }

  getAllRoles(): Observable<PagedResponseModel<RoleViewModel>> {
    return this.http.getData(`users/roles`);
  }

  getUserByID(userId): Observable<ResponseModel<UserViewModel>> {
    return this.http.getData(`users/${userId}`);
  }

  checkUserNameExists(username): Observable<ResponseModel<string>> {
    return this.http.getData(`users/check_username/${username}`);
  }

  checkEmailExists(email): Observable<ResponseModel<string>> {
    return this.http.getData(`users/check_email/${email}`);
  }

  checkPassword(userName, password): Observable<ResponseModel<boolean>> {
    return this.http.getData(`users/check_password/${userName}/${password}`);
  }

  createUser(user): Observable<ResponseModel<UserDtoModel>> {
    return this.http.postData('users', user);
  }

  updateUser(userId, user): Observable<ResponseModel<UserDtoModel>> {
    return this.http.putData(`users/${userId}`, user);
  }

  deleteUser(userId): Observable<ResponseModel<UserDtoModel>> {
    return this.http.deleteData(`users/delete_user/${userId}`);
  }
  getUsersWithoutEmployee(): Observable<UserViewModel[]> {
    return this.http.getData(`users/without_employee`);
  }
}
