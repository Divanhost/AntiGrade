import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Status } from 'src/app/shared/models/status.model';
import { Institute } from 'src/app/shared/models/institute.model';
import { Semester } from 'src/app/shared/models/semester.model';
import { Course } from 'src/app/shared/models/course.model';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  constructor(private http: HttpService) {

  }

  getCurrentMode(): Observable<ResponseModel<number>> {
    return this.http.getData(`general/mode`);
  }
  getAllStatuses(): Observable<ResponseModel<Status[]>> {
    return this.http.getData(`general/statuses`);
  }
  updateCurrentMode(id: number): Observable<ResponseModel<boolean>> {
    return this.http.getData(`general/mode/${id}`);
  }
  getInstitutes(): Observable<ResponseModel<Institute[]>> {
    return this.http.getData(`general/institutes`);
  }
  getInstitute(id: number): Observable<ResponseModel<Institute>> {
    return this.http.getData(`general/institutes/${id}`);
  }
  createInstitute(data: Institute): Observable<ResponseModel<boolean>> {
    return this.http.postData(`general/institutes`, data);
  }
  updateInstitute(data: Institute): Observable<ResponseModel<boolean>> {
    return this.http.putData(`general/institutes`, data);
  }
  deleteInstitute(id: number): Observable<ResponseModel<boolean>> {
    return this.http.getData(`general/institutes/${id}`);
  }

  getCourses(): Observable<ResponseModel<Course[]>> {
    return this.http.getData(`general/courses/all`);
  }
  getCourse(id: number): Observable<ResponseModel<Course>> {
    return this.http.getData(`general/courses/${id}`);
  }
  createCourse(data: Course): Observable<ResponseModel<boolean>> {
    return this.http.postData(`general/courses`, data);
  }
  updateCourse(data: Course): Observable<ResponseModel<boolean>> {
    return this.http.putData(`general/corses`, data);
  }
  deleteCourse(id: number): Observable<ResponseModel<boolean>> {
    return this.http.getData(`general/corses/${id}`);
  }

  getSemesters(): Observable<ResponseModel<Semester[]>> {
    return this.http.getData(`general/semesters`);
  }
  getLastSemester(): Observable<ResponseModel<Semester>> {
    return this.http.getData(`general/semesters/last`);
  }
  createSemester(data: Semester): Observable<ResponseModel<boolean>> {
    return this.http.postData(`general/semesters`, data);
  }
}
