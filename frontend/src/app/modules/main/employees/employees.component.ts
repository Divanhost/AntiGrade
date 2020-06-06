import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { BaseComponent } from 'src/app/shared/classes';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Employee } from 'src/app/shared/models/employee.model';
import { GeneralService } from 'src/app/core/services/general.service';
import { Institute } from 'src/app/shared/models/institute.model';
import { Department } from 'src/app/shared/models/department.model';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent extends BaseComponent implements OnInit {
  employees: Employee[] = [];
  institutes: Institute[] = [];
  currentInstitute: Institute = new Institute();
  currentDepartment: Department;
  constructor(
    private readonly employeeService: EmployeeService,
    private readonly generalService: GeneralService) {
    super();
  }

  ngOnInit(): void {
    this.getEmployees();
    this.getInstitutes();
  }

  getEmployees() {
    this.subscriptions.push(
      this.employeeService.getAllEmployees().subscribe((response: ResponseModel<Employee[]>) => {
        this.employees = response.payload;
      })
    );
  }

  getInstitutes() {
    this.subscriptions.push(
      this.generalService.getInstitutes().subscribe((response: ResponseModel<Institute[]>) => {
        this.institutes = response.payload;
        this.currentInstitute = this.institutes[0];
      })
    );
  }
  getDepartmentEmployees(department: Department) {
    this.subscriptions.push(
      this.employeeService.getDepartmentEmployees(department.id).subscribe((response: ResponseModel<Employee[]>) => {
        this.employees = response.payload;
      })
    );
  }
}
