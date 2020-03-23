import { Injectable } from '@angular/core';
import { Group } from 'src/app/shared/models/group.model';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http: HttpService) {

  }

  addGroup(data: Group): Observable<ResponseModel<boolean>> {
    return this.http.postData(`group`, data);
  }
  getGroups(): Observable<ResponseModel<Group[]>> {
    return this.http.getData(`group`);
  }
  getGroupsBySubjectId(id: number): Observable<ResponseModel<Group[]>> {
    return this.http.getData(`group/subject/${id}`);
  }
  deleteGroup(id: number): Observable<ResponseModel<boolean>> {
    return this.http.deleteData(`group/${id}`);
  }
}