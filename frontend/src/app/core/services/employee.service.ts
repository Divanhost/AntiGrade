import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { PagedResponseModel } from 'src/app/shared/models/paged-response.model';
import { Employee } from 'src/app/shared/models/employee.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { EmployeeDtoModel } from 'src/app/shared/models/employee-dto.model';
import { EmployeePosition } from 'src/app/shared/models/employee-position.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpService) {

  }

  getAllEmployees(): Observable<ResponseModel<Employee[]>> {
    return this.http.getData(`employee/all`);
  }
  getDepartmentEmployees(id: number): Observable<ResponseModel<Employee[]>> {
    return this.http.getData(`employee/department/${id}`);
  }
  getEmployeeById(id: number): Observable<ResponseModel<Employee>> {
    return this.http.getData(`employee/${id}`);
  }
  getEmployeesList(ids: number[]): Observable<ResponseModel<Employee[]>> {
    const params = HttpService.toHttpParams(ids);
    return this.http.getData(`employee/list`, params);
  }
  createEmployee(data: EmployeeDtoModel): Observable<boolean> {
    return this.http.postData(`employee`, data);
  }
  getEmployeePositions(): Observable<EmployeePosition[]> {
    return this.http.getData(`employee/positions`);
  }
  getSubjectEmployees(id: number): Observable<ResponseModel<SubjectEmployee[]>> {
    return this.http.getData(`employee/subject/${id}`);
  }
}
