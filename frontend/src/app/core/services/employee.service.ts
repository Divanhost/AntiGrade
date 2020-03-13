import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { PagedResponseModel } from 'src/app/shared/models/paged-response.model';
import { Employee } from 'src/app/shared/models/employee.model';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpService) {

  }

  getAllTeachers(): Observable<ResponseModel<Employee[]>> {
    return this.http.getData(`employee/teachers`);
  }
}
