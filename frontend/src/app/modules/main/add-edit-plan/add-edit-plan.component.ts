import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Work } from 'src/app/shared/models/work.model';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';
import { ExamType } from 'src/app/shared/models/exam-type.model';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  @Input() subjectId: number;
  @Input() currentExamType: ExamType;
  @Input() subject: SubjectDto = new SubjectDto();
  @Input() disabled: boolean;
  workTypes = [{id: 1, value: 'Лекция'}, {id: 2, value: 'Практика'}, {id: 3, value: 'Лабораторная'}];
  works: Work[] = [];
  constructor() {
    super();
  }
  get maxWorksPoints() {
    if (this.currentExamType) {
      switch (this.currentExamType.id) {
        case 1:
          return 60;
        case 2:
          return 100;
        case 3:
          return 100;
        default:
          return 100;
      }
    }
  }
  get currentWorkPoints() {
    let sum = 0;
    this.works.forEach(work => {
      sum += work.points;
    });
    return sum;
  }

  get isSumCorrect() {
    return this.currentWorkPoints === this.maxWorksPoints;
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
  isSumOfCriteriasCorrect(work: Work) {
    if (work.criterias.length) {
      let sum = 0;
      work.criterias.forEach(criteria => {
        sum += criteria.points;
      });
      return sum === work.points;
    } else {
      return true;
    }
  }
  removeCriteria(work: Work, criteria: Criteria) {
    const index = work.criterias.indexOf(criteria);
    work.criterias.splice(index, 1);
  }
  getWorksData() {
    if(this.subject.works) {
      const bonus = this.subject.works.filter(x => x.workTypeId === 4);
      return this.works.filter(x => x.name !== null && x.points !== null && x.workTypeId !== null).concat(bonus);
    } else {
      return this.works.filter(x => x.name !== null && x.points !== null && x.workTypeId !== null);
    }
  }
  updateData(subject: SubjectDto) {
    this.subject = subject;
    this.works = [];
    if(subject.works) {
      this.works = this.subject.works.filter(x => x.workTypeId !== 4);
    }
    if (!this.works.length) {
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
  hasErrors() {
    if (!this.isSumCorrect) {
      return true;
    }
    this.works.forEach(element => {
      if (!this.isSumOfCriteriasCorrect(element)) {
        return true;
      }
    });
    return false;
  }
}
