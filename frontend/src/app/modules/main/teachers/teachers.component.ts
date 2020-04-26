import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Employee } from 'src/app/shared/models/employee.model';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { faTrashAlt} from '@fortawesome/free-solid-svg-icons';
import { Status } from 'src/app/shared/models/status.model';
import { GeneralService } from 'src/app/core/services/general.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.scss']
})
export class TeachersComponent extends BaseComponent implements OnInit,OnChanges {
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  // @Output() changeData: EventEmitter<SubjectEmployee[]> = new EventEmitter();
  statusList = [{id: 1, name: 'Ответственный преподаватель'},
              {id: 2, name: 'Лектор'},
              {id: 3, name: 'Преподаватель практики'},
              {id: 4, name: 'Преподаватель лабораторных занятий'},
              {id: 5, name: 'Экзаменатор'}];
  // statusList = [];
  employees: Employee[] = [];
  subjectEmployees: SubjectEmployee[] = [];
  selectedEmployees: Employee[] = [];
  selectedStatuses = [];
  dropdownSettings = {
    singleSelection: false,
    enableCheckAll: false,
    idField: 'id',
    textField: 'fullName',
    itemsShowLimit: 3,
    allowSearchFilter: true
  };
  statusDropdownSettings = {
    singleSelection: false,
    enableCheckAll: false,
    idField: 'id',
    // textField: 'name',
    itemsShowLimit: 3,
    allowSearchFilter: true
  };
  faTrashAlt = faTrashAlt;
  constructor(private readonly employeeService: EmployeeService,
              private readonly generalService: GeneralService) {
    super();
  }
  ngOnInit(): void {
    if (this.subject.subjectEmployees) {
      this.subjectEmployees = this.subject.subjectEmployees;
    } else {
      this.subjectEmployees = [];
    }
    console.log(this.subjectEmployees);
    //this.getAllStatuses();
    this.getEmployees();

  }
  ngOnChanges() {
    console.log('subjectE', this.subjectEmployees);
  }
  getEmployees() {
    this.employeeService.getAllEmployees().subscribe((response: ResponseModel<Employee[]>) => {
      this.employees = response.payload;
      this.getSelectedEmployees();
    });
  }
  getAllStatuses() {
    this.subscriptions.push(
      this.generalService.getAllStatuses().subscribe((response: ResponseModel<Status[]>) => {
        this.statusList = response.payload;
      })
    );
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
      this.subjectEmployees.push({employeeId: employee.id, subjectId: this.subjectId, statuses: null});
    } else {
      this.subjectEmployees.push({employeeId: null, subjectId: this.subjectId, statuses: null});
    }
    console.log(this.subjectEmployees);
  }
  removeSubjectEmployee(teacher: SubjectEmployee) {
    const index = this.subjectEmployees.indexOf(teacher);
    this.subjectEmployees.splice(index, 1);
  }
  onItemSelect(item: Employee) {
    console.log(this.statusList);
    this.addSubjectEmployee(item);
  }
  onItemDeSelect(item: Employee) {
    this.subjectEmployees = this.subjectEmployees.filter(x => x.employeeId !== item.id);
  }
  onStatusSelect(teacher: SubjectEmployee, status: Status) {
    teacher.statuses.push(status);
  }
  onStatusDeSelect(teacher: SubjectEmployee, status: Status) {
    teacher.statuses = teacher.statuses.filter(x => x.id !== status.id);
  }
  getEmployeesData() {
    return this.subjectEmployees.filter(x => x.employeeId !== null && x.subjectId !== null && x.statuses.length);
  }
}
