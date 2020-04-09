import { Component, OnInit, ViewChild } from '@angular/core';
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
import { Group } from 'src/app/shared/models/group.model';
import { GroupService } from 'src/app/core/services/group.service';
import { SubjectCommonsComponent } from '../subject-commons/subject-commons.component';
import { AddEditPlanComponent } from '../add-edit-plan/add-edit-plan.component';
import { TeachersComponent } from '../teachers/teachers.component';
import { Work } from 'src/app/shared/models/work.model';
import { NotifierService } from 'angular-notifier';


@Component({
  selector: 'app-add-edit-subject',
  templateUrl: './add-edit-subject.component.html',
  styleUrls: ['./add-edit-subject.component.scss']
})
export class AddEditSubjectComponent extends BaseFormComponent implements OnInit {
  @ViewChild(SubjectCommonsComponent) subjectCommonsComponent: SubjectCommonsComponent;
  @ViewChild(AddEditPlanComponent) planComponent: AddEditPlanComponent;
  @ViewChild(TeachersComponent) teachersComponent: TeachersComponent;
  isCommonsVisible = true;
  isPlanVisible = false;
  isTeachersVisible = false;
  isCreate: boolean;
  subjectId: number;
  subject: SubjectDto = new SubjectDto();
  teachers: Employee[] = [];
  works: Work[] = [];
  group: Group = new Group();
  mainTeacher: Employee = new Employee();
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly employeeService: EmployeeService,
              private readonly subjectService: SubjectService,
              private readonly groupService: GroupService,
              private readonly notifierService: NotifierService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
    this.isCreate = this.subjectId === undefined;
  }

  ngOnInit(): void {

  }
  toggleCommons() {
    if (this.isPlanVisible) {
      this.togglePlan();
    }
    if (this.isTeachersVisible) {
      this.toggleTeachers();
    }
    this.isCommonsVisible = ! this.isCommonsVisible;
  }
  togglePlan() {
    if (this.isCommonsVisible) {
      this.toggleCommons();
    }
    if (this.isTeachersVisible) {
      this.toggleTeachers();
    }
    this.isPlanVisible = ! this.isPlanVisible;
  }
  toggleTeachers() {
    if (this.isPlanVisible) {
      this.togglePlan();
    }
    if (this.isCommonsVisible) {
      this.toggleCommons();
    }
    this.isTeachersVisible = ! this.isTeachersVisible;

  }
  changeSubject(subjectDto: SubjectDto) {
    this.subject.name = subjectDto.name;
    this.subject.examType = subjectDto.examType;
    this.subject.group = subjectDto.group;
    console.log(this.subject);
  }
  changeTeachers(teachers: SubjectEmployee[]) {
    this.subject.subjectEmployees = teachers;
    console.log(this.subject);
  }
  changeWorks(works: Work[]) {
    this.subject.works = works;
    console.log(this.subject);
  }
  createSubject() {
    this.subscriptions.push(
          this.subjectService.addSubject(this.subject).subscribe((response: ResponseModel<boolean>) => {
            if (response.payload) {
              this.notifierService.notify('success', 'Subject successfully created');
            } else {
              this.notifierService.notify('error', 'Cannot create subject');
            }
          })
        );
  }
}
