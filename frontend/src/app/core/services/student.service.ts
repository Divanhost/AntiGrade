import { Injectable } from '@angular/core';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { HttpService } from '.';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpService) {

  }

  getStudentCriterias(ids: number[]): Observable<ResponseModel<StudentCriteria[]>> {
    const params = HttpService.toHttpParams(ids);
    return this.http.getData(`student/criteria`, params);
  }

  updateStudentCriterias(data: StudentCriteria[]): Observable<ResponseModel<StudentCriteria[]>> {
    return this.http.putData(`student/criteria`, data);
  }
}
