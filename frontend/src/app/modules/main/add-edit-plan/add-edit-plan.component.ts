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
    this.updateData(this.subject);
  }
  addWork() {
    const work = new Work();
    work.name = null;
    work.points = null;
    work.workTypeId = null;
    work.criterias = [];
    work.subjectId = this.subject.id;
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
  getWorksData() {
    return this.works.filter(x => x.name !== null && x.points !== null && x.workTypeId !== null);
  }
  updateData(subject: SubjectDto) {
    this.subject = subject;
    this.works = this.subject.works;
    if (!this.works) {
      this.works = [];
      this.addWork();
    }
  }
  renewWorks() {
    if (this.works) {
      this.works.forEach(element => {
        element.id = 0;
        if (element.criterias) {
          element.criterias.forEach(criteria => {
            criteria.id = 0;
          });
        }
      });
    }
  }
}
