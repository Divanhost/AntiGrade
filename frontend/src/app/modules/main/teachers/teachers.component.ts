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
export class TeachersComponent extends BaseComponent implements OnInit {
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  @Input() disabled: boolean;

  statusList: Status[] = [];
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
    this.getAllStatuses();
    this.getEmployees();

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
        this.subjectEmployees.forEach(element => {
          element.statuses = this.statusList.filter(x => element.statuses.some(y => y.id === x.id));
        });
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
  }
  removeSubjectEmployee(teacher: SubjectEmployee) {
    const index = this.subjectEmployees.indexOf(teacher);
    this.subjectEmployees.splice(index, 1);
    this.selectedEmployees = this.selectedEmployees.filter(x => x.id !== teacher.employeeId);
  }
  onItemSelect(item: Employee) {
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
  updateData(subject: SubjectDto) {
    this.subject = subject;
    if (this.subject.subjectEmployees) {
      this.subjectEmployees = this.subject.subjectEmployees;
    } else {
      this.subjectEmployees = [];
    }
    this.getAllStatuses();
    this.getEmployees();
  }
}
