import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable, Subject } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { HttpHeaders } from '@angular/common/http';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Group } from 'src/app/shared/models/group.model';
import { SubjectGroup } from 'src/app/shared/models/subject-group.model';
import { MainSubjectView } from 'src/app/shared/models/main-subject-view.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpService) {

  }

  getExamTypes(): Observable<ResponseModel<ExamType[]>> {
    return this.http.getData(`subject/exam-types`);
  }
  addSubject(data: SubjectDto): Observable<ResponseModel<boolean>> {
    return this.http.postData(`subject`, data);
  }
  removeSubject(id: number): Observable<ResponseModel<boolean>> {
    return this.http.deleteData(`subject/${id}`);
  }
  getSubjects(): Observable<ResponseModel<SubjectView[]>> {
    return this.http.getData('subject/all');
  }
  getDistinctSubjects(): Observable<ResponseModel<MainSubjectView[]>> {
    return this.http.getData('subject/distinct');
  }
  getSubject(id: number): Observable<ResponseModel<SubjectDto>> {
    return this.http.getData(`subject/${id}`);
  }
  updateSubject(id: number, data: SubjectDto): Observable<ResponseModel<SubjectDto>> {
    return this.http.putData(`subject/${id}`, data);
  }
  addSubjectPlan(data: SubjectPlan): Observable<ResponseModel<boolean>> {
    return this.http.postData(`subject/plan`, data);
  }
  updateSubjectPlan(data: SubjectPlan): Observable<ResponseModel<boolean>> {
    return this.http.putData(`subject/plan`, data);
  }
  // getSubjectPlan(id: number): Observable<ResponseModel<boolean>> {
  //   return this.http.getData(`subject/plan/${id}`);
  // }
  getSubjectWorks(id: number): Observable<ResponseModel<Work[]>> {
    return this.http.getData(`subject/works/${id}`);
  }
  getSubjectStudents(id: number): Observable<ResponseModel<Student[]>> {
    return this.http.getData(`subject/students/${id}`);
  }
  updateSubjectGroups(id: number, data: SubjectGroup[]): Observable<ResponseModel<Group[]>> {
    return this.http.putData(`subject/groups/${id}`, data);
  }

}
