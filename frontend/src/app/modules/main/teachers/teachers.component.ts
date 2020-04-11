import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Employee } from 'src/app/shared/models/employee.model';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { faTrashAlt} from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.scss']
})
export class TeachersComponent extends BaseComponent implements OnInit {
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  // @Output() changeData: EventEmitter<SubjectEmployee[]> = new EventEmitter();
  statuses = ['Ответственный преподаватель', 'Лектор', 'Преподаватель практики', 'Преподаватель лабораторных занятий', 'Экзаменатор'];
  employees: Employee[] = [];
  subjectEmployees: SubjectEmployee[] = [];
  selectedEmployees: Employee[] = [];
  dropdownSettings = {
    singleSelection: false,
    enableCheckAll: false,
    idField: 'id',
    textField: 'fullName',
    itemsShowLimit: 3,
    allowSearchFilter: true
  };
  faTrashAlt = faTrashAlt;
  constructor(private readonly employeeService: EmployeeService) {
    super();
  }
  ngOnInit(): void {
    if (this.subject.subjectEmployees) {
      this.subjectEmployees = this.subject.subjectEmployees;
    } else {
      this.subjectEmployees = [];
    }
    this.getEmployees();
  }

  getEmployees() {
    this.employeeService.getAllEmployees().subscribe((response: ResponseModel<Employee[]>) => {
      this.employees = response.payload;
      this.getSelectedEmployees();
    });
  }

  getSelectedEmployees() {
    const selectedEmployeeIds = this.subjectEmployees.map(({ employeeId }) => employeeId);
    this.selectedEmployees = this.employees.filter(x => selectedEmployeeIds.includes(x.id));
  }
  getEmployeeById(id: number) {
    this.employeeService.getEmployeeById(id).subscribe((response: ResponseModel<Employee>) => {
      return response.payload;
    });
  }
  addSubjectEmployee(employee?) {
    if (employee) {
      this.subjectEmployees.push({employeeId: employee.id, subjectId: this.subjectId, status: null});
    } else {
      this.subjectEmployees.push({employeeId: null, subjectId: this.subjectId, status: null});
    }
  }
  removeSubjectEmployee(teacher: SubjectEmployee) {
    const index = this.subjectEmployees.indexOf(teacher);
    this.subjectEmployees.splice(index, 1);
  }
  onItemSelect(item: Employee) {
    this.addSubjectEmployee(item);
  }
  onItemDeSelect(item: Employee) {
    this.subjectEmployees = this.subjectEmployees.filter(x => x.employeeId !== item.id);
  }
  getEmployeesData() {
    return this.subjectEmployees.filter(x => x.employeeId !== null && x.subjectId !== null && x.status !== null);
  }
}
