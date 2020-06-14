import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Employee } from 'src/app/shared/models/employee.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectService } from 'src/app/core/services/subject.service';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { Group } from 'src/app/shared/models/group.model';
import { SubjectCommonsComponent } from '../subject-commons/subject-commons.component';
import { AddEditPlanComponent } from '../add-edit-plan/add-edit-plan.component';
import { TeachersComponent } from '../teachers/teachers.component';
import { Work } from 'src/app/shared/models/work.model';
import { NotifierService } from 'angular-notifier';
import { GroupComponent } from '../group/group.component';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { GeneralService } from 'src/app/core/services/general.service';
import { Semester } from 'src/app/shared/models/semester.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { EventEmitter } from 'protractor';


@Component({
  selector: 'app-add-edit-subject',
  templateUrl: './add-edit-subject.component.html',
  styleUrls: ['./add-edit-subject.component.scss']
})
export class AddEditSubjectComponent extends BaseFormComponent implements OnInit {
  @ViewChild(SubjectCommonsComponent) subjectCommonsComponent: SubjectCommonsComponent;
  @ViewChild(AddEditPlanComponent) planComponent: AddEditPlanComponent;
  @ViewChild(TeachersComponent) teachersComponent: TeachersComponent;
  @ViewChild(GroupComponent) groupComponent: GroupComponent;
  isCommonsVisible = true;
  isPlanVisible = false;
  isTeachersVisible = false;
  isGroupVisible = false;
  disabled = false;
  isCreate: boolean;
  subjectId: number;
  subject: SubjectDto;
  teachers: Employee[] = [];
  works: Work[] = [];
  group: Group = new Group();
  mainTeacher: Employee = new Employee();
  loaded = false;
  semesters: Semester[] = [];
  currentSemester: Semester = new Semester();
  filledSubjects: SubjectView[];
  
  currentExamType: ExamType = new ExamType();
  constructor(private readonly route: ActivatedRoute,
              private readonly router: Router,
              private readonly subjectService: SubjectService,
              private readonly generalService: GeneralService,
              private readonly notifierService: NotifierService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
    this.isCreate = this.subjectId === undefined;
  }

  ngOnInit(): void {
    this.getSemesters();
    if (!this.isCreate) {
      this.getSubject();
    } else {
      this.subject = new SubjectDto();
      this.loaded = true;
    }
  }
  toggleCommons() {
    if (this.isPlanVisible) {
      this.togglePlan();
    }
    if (this.isTeachersVisible) {
      this.toggleTeachers();
    }
    if (this.isGroupVisible) {
      this.toggleGroup();
    }
    this.isCommonsVisible = !this.isCommonsVisible;
  }
  togglePlan() {
    if (this.isCommonsVisible) {
      this.toggleCommons();
    }
    if (this.isTeachersVisible) {
      this.toggleTeachers();
    }
    if (this.isGroupVisible) {
      this.toggleGroup();
    }
    this.isPlanVisible = !this.isPlanVisible;
  }
  toggleTeachers() {
    if (this.isPlanVisible) {
      this.togglePlan();
    }
    if (this.isCommonsVisible) {
      this.toggleCommons();
    }
    if (this.isGroupVisible) {
      this.toggleGroup();
    }
    this.isTeachersVisible = !this.isTeachersVisible;
  }
  toggleGroup() {
    if (this.isPlanVisible) {
      this.togglePlan();
    }
    if (this.isCommonsVisible) {
      this.toggleCommons();
    }
    if (this.isTeachersVisible) {
      this.toggleTeachers();
    }
    this.isGroupVisible = !this.isGroupVisible;
  }

  createOrUpdateSubject() {
    this.uniteData();
    
  }
  createSubject() {
    this.subject.id = 0;
    this.subscriptions.push(
      this.subjectService.addSubject(this.subject).subscribe((response: ResponseModel<boolean>) => {
        if (response.payload) {
          this.notifierService.notify('success', 'Предмет успешно создан');
          this.router.navigate(['/subjects']);

        } else {
          this.notifierService.notify('error', 'Невозможно создать предмет');
        }
      })
    );
  }
  updateSubject() {
    this.subscriptions.push(
      this.subjectService.updateSubject(this.subjectId, this.subject).subscribe((response: ResponseModel<boolean>) => {
        if (response.payload) {
          this.notifierService.notify('success', 'Предмет успешно обновлен');
          this.router.navigate(['subjects/rating', this.subjectId]);
        } else {
          this.notifierService.notify('error', 'Невозможно создать предмет');
        }
      })
    );
  }
  getSubject() {

    this.subscriptions.push(
      this.subjectService.getSubject(this.subjectId).subscribe((response: ResponseModel<SubjectDto>) => {
        this.subject = response.payload;
        this.checkAvailability();
      })
    );
  }
  uniteData() {
    debugger
    this.subject.name = this.subjectCommonsComponent.name;
    if(!this.subject.name) {
      this.notifierService.notify('error','Нельзя создать предмет без названия')
      return;
    }
    this.subject.hasBonuses = this.subjectCommonsComponent.hasBonuses;
    this.subject.semesterId = this.subjectCommonsComponent.semesterId;
    this.subject.examType = this.subjectCommonsComponent.examType;
    if(!this.subject.name) {
      this.notifierService.notify('error','Нельзя создать предмет без вида контроля')
      return;
    }
    this.subject.works = this.planComponent.getWorksData();
    this.subject.subjectEmployees = this.teachersComponent.getEmployeesData();
    this.subject.group = this.groupComponent.getGroupData();
    if(!this.subject.group) {
      this.notifierService.notify('error','Необходимо добавить группу')
      return;
    }
    if (this.isCreate) {
      this.createSubject();
    } else {
      this.updateSubject();
    }
  }
  saveAndRedirect() {
    this.createOrUpdateSubject();
  }
  importPlan() {
    this.subjectService.getSubjectsWithWorks(this.currentSemester.id).subscribe((response: ResponseModel<SubjectView[]>) => {
      this.filledSubjects = response.payload;
      if (this.subjectId) {
        const index = this.filledSubjects.findIndex(x => x.id === +this.subjectId);
        this.filledSubjects.splice(index, 1);
      }
    });
  }
  getAnotherSubject(id: number) {
    this.subjectId = id;
    this.getSubject();

  }
  getSemesters() {
    this.subscriptions.push(
      this.generalService.getSemesters().subscribe((response: ResponseModel<Semester[]>) => {
        this.semesters = response.payload;
        this.currentSemester = this.semesters[0];
      })
    );
  }
  changeType(type: ExamType) {
    this.currentExamType = type;
  }
  checkAvailability() {
    this.subscriptions.push(
      this.subjectService.checkAvailability(this.subjectId).subscribe((response: ResponseModel<boolean>) => {
        this.disabled = response.payload;
        if(this.isCreate) {
          this.subject.works = this.subject.works.filter(x=>x.workTypeId !== 4);
        }
        this.loaded = true;
        this.subjectCommonsComponent.updateData(this.subject);
        this.planComponent.updateData(this.subject);
        this.planComponent.renewWorks();
        this.teachersComponent.updateData(this.subject);
        this.groupComponent.updateData(this.subject);
      })
    );
  }
}
