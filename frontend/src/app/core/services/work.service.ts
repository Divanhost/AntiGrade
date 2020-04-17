import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { Work } from 'src/app/shared/models/work.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { StudentWork } from 'src/app/shared/models/student-work.model';

@Injectable({
  providedIn: 'root'
})
export class WorkService {

  constructor(private http: HttpService) {

  }
  getStudentCriterias(id: number): Observable<ResponseModel<StudentCriteria[]>> {
    return this.http.getData(`work/criteria/${id}`);
  }

  updateStudentCriterias(data: StudentCriteria[]): Observable<ResponseModel<boolean>> {
    return this.http.putData(`work/criteria`, data);
  }

  getStudentWorks(id: number): Observable<ResponseModel<StudentWork[]>> {
    return this.http.getData(`work/studentworks/${id}`);
  }

  updateStudentWorks(data: StudentWork[]): Observable<ResponseModel<boolean>> {
    return this.http.putData(`work/studentworks`, data);
  }
}
