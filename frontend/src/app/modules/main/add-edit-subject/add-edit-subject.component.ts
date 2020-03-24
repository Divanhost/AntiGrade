import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseComponent, BaseFormComponent } from 'src/app/shared/classes';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Employee } from 'src/app/shared/models/employee.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectService } from 'src/app/core/services/subject.service';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';

@Component({
  selector: 'app-add-edit-subject',
  templateUrl: './add-edit-subject.component.html',
  styleUrls: ['./add-edit-subject.component.scss']
})
export class AddEditSubjectComponent extends BaseFormComponent implements OnInit {
  isCreate: boolean;
  subjectId: number;
  teachers: Employee[] = [];
  examTypes: ExamType[] = [];
  subject: SubjectDto = new SubjectDto();
  selectedEmployees: Employee[] = [];
  dropdownList = [];
  mainTeacher: Employee = new Employee();
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly employeeService: EmployeeService,
              private readonly subjectService: SubjectService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
    this.isCreate = this.subjectId === undefined;
  }

  ngOnInit(): void {
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'fullName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
    this.initForm();
    this.getExamTypes();
    this.getTeachers();
  }
  initForm() {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      examType: new FormControl('', [Validators.required]),
      teachers: new FormControl('', [Validators.required]),
      mainTeacher: new FormControl(null, Validators.required)
    });
  }
  getSubject() {
    this.subscriptions.push(
      this.subjectService.getSubject(this.subjectId).subscribe((response: ResponseModel<SubjectDto>) => {
        this.subject = response.payload;
        if (this.subject.subjectEmployees.length) {
          this.subject.subjectEmployees.forEach(element => {
            const teacher = this.teachers.find(x => x.id === element.employeeId);
            this.selectedEmployees.push(teacher);
            if (element.status === 'MT') {
              this.mainTeacher = teacher;
            }
          });
        }
        this.fillForm();
      })
    );
  }
  fillForm() {
    this.form.controls.name.setValue(this.subject.name);
    this.form.controls.examType.setValue(this.subject.examType);
    this.form.controls.teachers.setValue(this.selectedEmployees);
    this.form.controls.mainTeacher.setValue(this.mainTeacher);
  }
  onSubmit() {
    this.convertEmployees();
    if (this.isCreate) {
      this.subscriptions.push(
        this.subjectService.addSubject(this.subject).subscribe(() => {
          this.router.navigate(['/subjects']);
        })
      );
    } else {
      this.subscriptions.push(
        this.subjectService.updateSubject(this.subjectId, this.subject).subscribe(() => {
          this.router.navigate(['/subjects']);
        })
      );
    }
  }
  convertEmployees() {
    this.subject.subjectEmployees = [];
    this.selectedEmployees.forEach(element => {
      const se = new SubjectEmployee();
      se.subjectId = +this.subjectId || 0;
      se.employeeId = element.id;
      this.subject.subjectEmployees.push(se);
    });
    this.subject.subjectEmployees.find(x => x.employeeId === this.mainTeacher.id).status = 'MT';
  }
  getExamTypes() {
    this.subscriptions.push(
      this.subjectService.getExamTypes().subscribe((responce: ResponseModel<ExamType[]>) => {
        this.examTypes = responce.payload;
      })
    );
  }
  getTeachers() {
    this.subscriptions.push(
      this.employeeService.getAllTeachers().subscribe((responce: ResponseModel<Employee[]>) => {
        this.teachers = responce.payload;
        if (!this.isCreate) {
          this.getSubject();
        }
      })
    );
  }
}
