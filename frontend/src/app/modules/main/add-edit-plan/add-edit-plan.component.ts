import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { SubjectPlan } from 'src/app/shared/models/subject-plan.model';
import { SubjectService } from 'src/app/core/services/subject.service';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {

  isCreate: boolean;
  subjectId: number;
  plan: SubjectPlan = new SubjectPlan();
  students: Student[] = [];
  isCriteraisShown = true;
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly subjectService: SubjectService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
    this.isCreate = this.subjectId === undefined;
  }

  ngOnInit(): void {
    this.plan.works = [];
    this.addWork();
    this.plan.subjectId = this.subjectId;
    this.students.push({name: 'Angelina'});
    this.students.push({name: 'George'});
    this.students.push({name: 'Ivan'});
  }
  addWork() {
    const work = new Work();
    work.criterias = [];
    this.plan.works.push(work);
    this.toggleCriterias();
  }
  addCriteria(work: Work) {
    work.criterias.push(new Criteria());
  }
  savePlan() {
    console.log(this.plan.works);
    this.subscriptions.push(
      this.subjectService.addSubjectPlan(this.plan).subscribe()
    );
  }
  toggleCriterias() {
    this.isCriteraisShown = !this.isCriteraisShown;
    console.log(this.isCriteraisShown);
  }
}
