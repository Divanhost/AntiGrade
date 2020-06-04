import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentWork } from 'src/app/shared/models/student-work.model';
import { BaseComponent } from 'src/app/shared/classes';
import { WorkService } from 'src/app/core/services/work.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { CriteriasComponent } from '../criterias/criterias.component';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { Mode } from 'src/app/shared/models/mode.model';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-partial-table',
  templateUrl: './partial-table.component.html',
  styleUrls: ['./partial-table.component.scss']
})
export class PartialTableComponent extends BaseComponent implements OnInit {
  @Input() title: string;
  @Input() subjectId: number;
  @Input() works: Work[] = [];
  @Input() students: Student[] = [];
  @Input() studentWorks: StudentWork[] = [];
  @Input() disabled = false;
  @Input() isAdditionalPageMode = false;
  @Input() mode: Mode;
  @Output() changeData = new EventEmitter<StudentWork>();
  @ViewChild(CriteriasComponent) criteriaComponent: CriteriasComponent;
  selectedWork: Work = new Work();
  faPencilAlt = faPencilAlt;

  data = [];
  selected = false;
  get isExamMode() {
    if (this.mode) {
      return this.mode.id === 2;
    }
  }
  get isAdditionalMode() {
    if (this.mode) {
      return this.mode.id === 3;
    }
  }
  get isExamOrAdditionalMode() {
    return this.isAdditionalMode || this.isExamMode;
  }
  constructor(private readonly workService: WorkService,
              private readonly notifier: NotifierService,
              private readonly modalService: NgbModal) {
    super();
  }

  ngOnInit(): void {
    this.createRatingCells();
  }

  createRatingCells() {
    let row = [];
    this.data = [];
    this.students.forEach(student => {
      this.works.forEach(work => {
        const sp = this.studentWorks.find(x => x.workId === work.id && x.studentId === student.id);
        if (sp) {
          row.push(sp);
        } else {
          const studentWork = new StudentWork();
          studentWork.workId = work.id;
          studentWork.studentId = student.id;
          row.push(studentWork);
        }
      });
      this.data.push({studentWorks: row, currentStudent: student});
      row = [];
    });
  }

  updateWorkPoints(studentWork: StudentWork, event: any) {
    const editField = event.target.textContent.replace(/\s/g, '');
    const work = this.works.find(x => x.id === studentWork.workId);
    const numberReSnippet = '(?:NaN|-?(?:(?:\\d+|\\d*\\.\\d+)(?:[E|e][+|-]?\\d+)?|Infinity))';
    const matchOnlyNumberRe = new RegExp('^(' + numberReSnippet + ')$');
    if (!work.canBeQuickRated) {
      event.target.textContent = 0;
      this.notifier.notify('error', 'Работа может быть оценена только с помощью критериев');
      return;
    }
    if ( +editField > work.points || !matchOnlyNumberRe.test(editField)) {
      if (!editField) {
        return;
      }
      event.target.textContent = 0;
      this.notifier.notify('error', 'Неверно введены баллы');
      return;
    }

    studentWork.sumOfPoints = +editField;
    const hasWork = this.studentWorks.find(x => x.workId === studentWork.workId && x.studentId === studentWork.studentId);
    if (!hasWork) {
      this.studentWorks.push(studentWork);
    }
    this.createRatingCells();
    this.changeData.emit(studentWork);
  }

  selectWork(workId) {
    this.selectedWork = this.works.find(x => x.id === workId);
    this.open();
  }
  open() {
    const modalRef = this.modalService.open(CriteriasComponent);
    modalRef.componentInstance.workId = this.selectedWork.id;
    modalRef.componentInstance.criterias = this.selectedWork.criterias;
    modalRef.componentInstance.students = this.students;
    modalRef.componentInstance.changeData.subscribe((receivedEntry: StudentWork[]) => {
      this.updateData(receivedEntry);
      this.selectedWork.canBeQuickRated = false;
      this.selectedWork.additionalsCanBeQuickRated = false;
    });
  }
  updateData(studentWorks: StudentWork[]) {
    studentWorks.forEach(element => {
    const swList = this.data.find(x => x.currentStudent.id === element.studentId).studentWorks;
    const sw  = swList.find(x => x.workId === element.workId);
    sw.sumOfPoints = element.sumOfPoints;
    const hasWork = this.studentWorks.find(x => x.workId === element.workId && x.studentId === element.studentId);
    if (!hasWork) {
      this.studentWorks.push(element);
    }
    });
  }
  canRate(work: Work) {
    if (this.mode.id === 1) {
      return work.canBeQuickRated;
    } else {
      return work.additionalsCanBeQuickRated;
    }
  }
  get isDisabled() {
    if (this.disabled) {
      return true;
    }
    if (this.isExamMode) {
      return true;
    }
    if (this.isAdditionalPageMode && this.isAdditionalMode) {
      return false;
    }
    if (this.isAdditionalMode) {
      return true;
    }
  }
}
