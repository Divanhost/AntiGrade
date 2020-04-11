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
import { faPlusSquare, faMinusSquare, faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  workTypes = [{id: 1, value: 'Лекция'}, {id: 2, value: 'Практика'}, {id: 3, value: 'Лабораторная'}];
  works: Work[] = [];
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly subjectService: SubjectService) {
    super();
  }

  ngOnInit(): void {
    this.works = this.subject.works;
    if (!this.works) {
      this.works = [];
      this.addWork();
    }
  }
  addWork() {
    const work = new Work();
    work.name = null;
    work.points = null;
    work.workTypeId = null;
    work.criterias = [];
    work.subjectId = this.subjectId;
    this.works.push(work);
  }

  addCriteria(work: Work) {
    const criteria = new Criteria();
    criteria.workId = work.id;
    criteria.name = null;
    work.criterias.push(criteria);
  }

  removeCriteria(work: Work, criteria: Criteria) {
    const index = work.criterias.indexOf(criteria);
    work.criterias.splice(index, 1);
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
   // this.changeData.emit(this.works);
   console.log(this.works);
  }
  getWorksData() {
    return this.works.filter(x => x.name !== null && x.points !== null && x.workTypeId !== null);
  }
}
