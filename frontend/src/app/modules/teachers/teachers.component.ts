import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/shared/models/employee.model';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';
import { BaseComponent } from 'src/app/shared/classes';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.scss']
})
export class TeachersComponent extends BaseComponent implements OnInit {
  subjectId: number;
  statuses = ['Лектор', 'Преподаватель практики', 'Преподаватель лабораторных занятий', 'Экзаменатор'];
  teachers: Employee[] = [];
  mainTeacher: Employee = new Employee();
  subjectEmployees: SubjectEmployee[] = [];
  selectedTeachers: Employee[] = [];
  constructor(private readonly employeeService: EmployeeService,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.getTeachers();
  }
  getTeachers() {
    this.employeeService.getAllEmployees().subscribe((response: ResponseModel<Employee[]>) => {
      this.teachers = response.payload;
      console.log(this.teachers);
      this.getSubjectEmployees();
    });
  }
  getSubjectEmployees() {
    this.employeeService.getSubjectEmployees(this.subjectId).subscribe((response: ResponseModel<SubjectEmployee[]>) => {
      this.subjectEmployees = response.payload;
      const mTId = this.subjectEmployees.find(x => x.status === 'MT').employeeId;
      this.mainTeacher = this.teachers.find(x => x.id === mTId);
      console.log(this.mainTeacher);
      console.log(this.subjectEmployees);
      this.getEmployees();
    });
  }
  getEmployees() {
    const employeeIds = this.subjectEmployees.map(({ employeeId }) => employeeId);
    this.employeeService.getEmployeesList(employeeIds).subscribe((response: ResponseModel<Employee[]>) => {
      this.selectedTeachers = response.payload;
      console.log(this.selectedTeachers);
    });
  }
  addSubjectEmployee() {
    this.subjectEmployees.push({employeeId: null, subjectId: this.subjectId, status: null});
  }
  removeSubjectEmployee(teacher: SubjectEmployee) {
    const index = this.subjectEmployees.indexOf(teacher);
    this.subjectEmployees.splice(index, 1);
    // this.subjectEmployees.filter(x => x !== teacher); // ({employeeId: null, subjectId: this.subjectId, status: null});
  }
}
