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
import { Totals } from 'src/app/shared/models/totals.model';
import { ExamResult } from 'src/app/shared/models/exam-result.model';
import { Status } from 'src/app/shared/models/status.model';

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
  getSubjectsByName(name: string): Observable<ResponseModel<SubjectView[]>> {
    return this.http.getData(`subject/name?name=${name}`);
  }
  updateSubject(id: number, data: SubjectDto): Observable<ResponseModel<boolean>> {
    return this.http.putData(`subject/${id}`, data);
  }

  getSubjectWorks(id: number): Observable<ResponseModel<Work[]>> {
    return this.http.getData(`subject/works/${id}`);
  }
  getSubjectStudents(id: number): Observable<ResponseModel<Student[]>> {
    return this.http.getData(`subject/students/${id}`);
  }
  updateSubjectGroups(id: number, data: SubjectGroup[]): Observable<ResponseModel<Group[]>> {
    return this.http.putData(`subject/groups/${id}`, data);
  }
  getSubjectTotals(id: number, ids: number[]): Observable<ResponseModel<Totals[]>> {
    const params = HttpService.toHttpParams({studentIds: ids});
    return this.http.getData(`subject/total/${id}?${params}`);
  }
  getSubjectAdditionalTotals(id: number, ids: number[]): Observable<ResponseModel<Totals[]>> {
    const params = HttpService.toHttpParams({studentIds: ids});
    return this.http.getData(`subject/total/additional/${id}?${params}`);
  }
  getExamResults(id: number, ids: number[]): Observable<ResponseModel<ExamResult[]>> {
    const params = HttpService.toHttpParams({studentIds: ids});
    return this.http.getData(`subject/exam/${id}?${params}`);
  }
  updateExamResults(data: ExamResult[]): Observable<ResponseModel<boolean>> {
    return this.http.putData(`subject/exam`, data);
  }
  getRoles(subjectId: number, employeeId: number): Observable<ResponseModel<Status[]>> {
    return this.http.getData(`subject/roles?subjectId=${subjectId}&employeeId=${employeeId}`);
  }
}
