import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { PagedResponseModel } from 'src/app/shared/models/paged-response.model';
import { Employee } from 'src/app/shared/models/employee.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpService) {

  }

  getExamTypes(): Observable<ResponseModel<ExamType[]>> {
    return this.http.getData(`subject/exam-types`);
  }
}
