import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { Employee } from 'src/app/shared/models/employee.model';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { UserService } from 'src/app/core/services';
import { UserViewModel } from 'src/app/shared/models';
import { EmployeeDtoModel } from 'src/app/shared/models/employee-dto.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { EmployeePosition } from 'src/app/shared/models/employee-position.model';
import { Institute } from 'src/app/shared/models/institute.model';
import { Department } from 'src/app/shared/models/department.model';
import { GeneralService } from 'src/app/core/services/general.service';

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.scss']
})
export class AddEditEmployeeComponent extends BaseComponent implements OnInit {

  isCreate: boolean;
  employeeId: number;
  employee: Employee = new Employee();
  users: UserViewModel[] = [];
  employeePositions: EmployeePosition[] = [];
  dropdownSettings = {};
  institutes: Institute[] = [];
  currentInstitute: Institute = new Institute();
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly employeeService: EmployeeService,
              private readonly userService: UserService,
              private readonly generalService: GeneralService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.employeeId = params.id)
    );
    this.isCreate = this.employeeId === undefined;
  }
  ngOnInit(): void {
    this.getUsersWithoutEmployee();
    this.getInstitutes();
    if (!this.isCreate) {
      this.getEmployee();
    }
  }

  getUsersWithoutEmployee() {
    this.subscriptions.push(
      this.userService
        .getUsersWithoutEmployee()
        .subscribe((response: UserViewModel[]) => {
          this.users = response;
        })
    );
  }
  getEmployee() {
    this.subscriptions.push(
      this.employeeService
        .getEmployeeById(this.employeeId)
        .subscribe((response: ResponseModel<Employee>) => {
          this.employee = response.payload;
        })
    );
  }
  getInstitutes() {
    this.subscriptions.push(
      this.generalService.getInstitutes().subscribe((response: ResponseModel<Institute[]>) => {
        this.institutes = response.payload;
        this.currentInstitute = this.institutes[0];
        console.log(this.institutes);
      })
    );
  }

  createEmployee() {
    this.subscriptions.push(
      this.employeeService.createEmployee(this.employee).subscribe()
    );
  }
}
