import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { HttpHeaders } from '@angular/common/http';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Group } from 'src/app/shared/models/group.model';

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
  getSubjects(): Observable<ResponseModel<SubjectView[]>> {
    return this.http.getData('subject/all');
  }
  addSubjectPlan(data: SubjectPlan): Observable<ResponseModel<boolean>> {
    return this.http.postData(`subject/plan`, data);
  }
  getSubjectWorks(id: number): Observable<ResponseModel<Work[]>> {
    return this.http.getData(`subject/works/${id}`);
  }
  getSubjectStudents(id: number): Observable<ResponseModel<Student[]>> {
    return this.http.getData(`subject/students/${id}`);
  }

  updateSubjectGroups(id: number, groups: Group[]): Observable<ResponseModel<Group[]>> {
    return this.http.putData(`subject/groups/${id}`, groups);
  }

}