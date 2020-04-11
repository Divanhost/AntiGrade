import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {

  isCreate: boolean;
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  @Output() changeData: EventEmitter<Work[]> = new EventEmitter();
  mode: string;
  workTypes = [{id: 1, value: 'Лекция'}, {id: 2, value: 'Практика'}, {id: 3, value: 'Лабораторная'}];
  // plan: SubjectPlan = new SubjectPlan();
  works: Work[] = [];
  students: Student[] = [];
  isCriteraisShown = false;
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly subjectService: SubjectService) {
    super();
  }

  ngOnInit(): void {
    // if (!this.isCreate) {
    //   this.getPlan();
    // } else {
    //   this.plan.works = [];
    //   this.addWork();
    // }
    this.works = this.subject.works;
    this.addWork();
  }
  addWork() {
    const work = new Work();
    work.criterias = [];
    work.subjectId = this.subjectId;
    this.addCriteria(work);
    this.works.push(work);
  }
  // getPlan() {
  //   this.subscriptions.push(
  //     this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
  //       this.plan.works = response.payload;
  //       this.plan.works.forEach(element => {
  //         if (!element.criterias.length) {
  //           this.addCriteria(element);
  //         }
  //       });
  //     })
  //   );
  // }
  addCriteria(work: Work) {
    const criteria = new Criteria();
    criteria.workId = work.id;
    criteria.name = '';
    work.criterias.push(criteria);
  }
  removeCriteria(work: Work, criteria: Criteria) {
    work.criterias.filter(x => x !== criteria);
  }
  savePlan() {
    // const plan = Object.assign(this.plan);
    // plan.works = plan.works.filter(x => x.name !== '' && x.name !== null);
    // plan.works.forEach(element => {
    //     element.criterias = element.criterias.filter(x => x.name !== '' &&  x.name !== null);
    // });
    // if (this.isCreate) {
    //   this.subscriptions.push(
    //     this.subjectService.addSubjectPlan(plan).subscribe(() => {
    //       this.router.navigate(['/subjects']);
    //     })
    //   );
    // } else {
    //   this.subscriptions.push(
    //     this.subjectService.updateSubjectPlan(plan).subscribe(() => {
    //       this.router.navigate(['/subjects']);
    //     })
    //   );
    // }
    this.changeData.emit(this.works);
  }
  toggleCriterias() {
    this.isCriteraisShown = !this.isCriteraisShown;
  }
}
