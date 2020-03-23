import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {

  isCreate: boolean;
  subjectId: number;
  mode: string;
  plan: SubjectPlan = new SubjectPlan();
  students: Student[] = [];
  isCriteraisShown = true;
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly subjectService: SubjectService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id),
      this.route.url.subscribe(params => this.mode = params[1].path)
    );
    this.isCreate = this.mode === 'add';
  }

  ngOnInit(): void {
    if (!this.isCreate) {
      this.getPlan();
    } else {
      this.plan.works = [];
      this.addWork();
    }
    this.plan.subjectId = this.subjectId;
  }
  addWork() {
    const work = new Work();
    work.criterias = [];
    this.addCriteria(work);
    this.plan.works.push(work);
    this.isCriteraisShown = false;
  }
  getPlan() {
    this.subscriptions.push(
      this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
        this.plan.works = response.payload;
      })
    );
  }
  addCriteria(work: Work) {
    const criteria = new Criteria();
    criteria.workId = work.id;
    criteria.name = '';
    work.
    criterias.push(criteria);
  }
  removeCriteria(work: Work, criteria: Criteria) {
    work.criterias.filter(x => x !== criteria);
  }
  savePlan() {
    this.plan.works.forEach(element => {
      element.criterias.filter(x => x.name !== '')
    });
    if (this.isCreate) {
      this.subscriptions.push(
        this.subjectService.addSubjectPlan(this.plan).subscribe()
      );
    } else {
      this.subscriptions.push(
        this.subjectService.updateSubjectPlan(this.plan).subscribe()
      );
    }
  }
  toggleCriterias() {
    this.isCriteraisShown = !this.isCriteraisShown;
  }
}
