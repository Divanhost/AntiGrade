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

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.scss']
})
export class AddEditEmployeeComponent extends BaseComponent implements OnInit {

  isCreate: boolean;
  employeeId: number;
  employee: EmployeeDtoModel = new EmployeeDtoModel();
  users: UserViewModel[] = [];
  employeePositions: EmployeePosition[] = [];
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly employeeService: EmployeeService,
              private readonly userService: UserService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.employeeId = params.id)
    );
    this.isCreate = this.employeeId === undefined;
  }
  ngOnInit(): void {
    this.getUsersWithoutEmployee();
    this.getEmployeePositions();
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
        .subscribe((response: ResponseModel<EmployeeDtoModel>) => {
          this.employee = response.payload;
        })
    );
  }
  getEmployeePositions() {
    this.subscriptions.push(
      this.employeeService
        .getEmployeePositions()
        .subscribe((response) => {
          debugger;
          this.employeePositions = response.payload;
        })
    );
  }
  createEmployee() {
    this.subscriptions.push(
      this.employeeService.createEmployee(this.employee).subscribe()
    );
  }
}
