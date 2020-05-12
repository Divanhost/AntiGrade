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
  @Input() mode: Mode;
  @Output() changeData = new EventEmitter<StudentWork>();
  @ViewChild(CriteriasComponent) criteriaComponent: CriteriasComponent;
  selectedWork: Work = new Work();
  regex = new RegExp('^-?[0-9][0-9,\.]+$');
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
    const editField = event.target.textContent;
    const work = this.works.find(x => x.id === studentWork.workId);
    if ( editField > work.points /*|| !this.regex.test(editField)*/) {
      event.target.classList.add('off-limits');
      return;
    } else {
      event.target.classList.remove('off-limits');
    }
    studentWork.sumOfPoints = +editField;
    if ( studentWork.sumOfPoints.toString() !== '' && studentWork.sumOfPoints !== null) {
      const hasWork = this.studentWorks.find(x => x.workId === studentWork.workId && x.studentId === studentWork.studentId);
      if (!hasWork) {
        this.studentWorks.push(studentWork);
      }
    } else {
      studentWork.sumOfPoints = 0;
    }
    this.createRatingCells();
    this.changeData.emit(studentWork);
  }

  // getStudentWorks() {
  //   this.subscriptions.push(
  //     this.workService.getStudentWorks(this.subjectId).subscribe((response: ResponseModel<StudentWork[]>) => {
  //       this.studentWorks = response.payload;
  //       // this.changeData.emit(this.studentWorks);
  //       this.createRatingCells();
  //     })
  //   );
  // }

  // getAdditionalStudentWorks() {
  //   this.subscriptions.push(
  //     this.workService.getAdditionalStudentWorks(this.subjectId).subscribe((response: ResponseModel<StudentWork[]>) => {
  //       this.studentWorks = response.payload;
  //       // this.changeData.emit(this.studentWorks);
  //       this.createRatingCells();
  //     })
  //   );
  // }

  // updateStudentWorks() {
  //   if (this.mode.id === 3) {
  //     this.studentWorks.forEach(element => {
  //       debugger;
  //       element.isAdditional = true;
  //     });
  //   }
  //   this.subscriptions.push(
  //     this.workService.updateStudentWorks(this.studentWorks).subscribe(() => {
  //     })
  //   );
  // }
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

}
