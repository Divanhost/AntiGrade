import { Injectable } from '@angular/core';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { HttpService } from '.';
import { StudentWork } from 'src/app/shared/models/student-work.model';

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

  updateStudentCriterias(data: StudentCriteria[]): Observable<ResponseModel<boolean>> {
    return this.http.putData(`student/criteria`, data);
  }

  getStudentWorks(ids: number[]): Observable<ResponseModel<StudentWork[]>> {
    const params = HttpService.toHttpParams({studentIds: ids});
    return this.http.getData(`student/works?${params}`);
  }

  updateStudentWorks(data: StudentWork[]): Observable<ResponseModel<boolean>> {
    return this.http.putData(`student/works`, data);
  }
}
