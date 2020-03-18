import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { Work } from 'src/app/shared/models/work.model';

@Injectable({
  providedIn: 'root'
})
export class WorkService {

  constructor(private http: HttpService) {

  }


}
