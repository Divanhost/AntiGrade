import { Component, OnInit, Input } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentWork } from 'src/app/shared/models/student-work.model';
import { BaseComponent } from 'src/app/shared/classes';
import { WorkService } from 'src/app/core/services/work.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';

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
  @Input() disabled = false;
  regex = new RegExp('^-?[0-9][0-9,\.]+$');
  studentWorks: StudentWork[] = [];
  data = [];
  constructor(private readonly workService: WorkService) {
    super();
  }

  ngOnInit(): void {
    this.getStudentWorks();
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
    if ( editField > work.points || !this.regex.test(editField)) {
      event.target.classList.add('off-limits');
      return;
    } else {
      event.target.classList.remove('off-limits');
    }
    studentWork.sumOfPoints = editField;
    if ( studentWork.sumOfPoints.toString() !== '' && studentWork.sumOfPoints !== null) {
      const hasWork = this.studentWorks.find(x => x.workId === studentWork.workId && x.studentId === studentWork.studentId);
      if (!hasWork) {
        this.studentWorks.push(studentWork);
      }
    } else {
      studentWork.sumOfPoints = 0;
    }
    this.createRatingCells();
  }
  // getStudentCriteria() {
  //   const studentIds = this.students.map(({ id }) => id);
  //   this.subscriptions.push(
  //     this.workService.getStudentCriterias(studentIds).subscribe((response: ResponseModel<StudentCriteria[]>) => {
  //       this.studentCriteria = response.payload;
  //     })
  //   );
  // }
  // updateStudentCriteria() {
  //   this.subscriptions.push(
  //     this.workService.updateStudentCriterias(this.studentCriteria).subscribe()
  //   );
  // }

  getStudentWorks() {
    const workIds = this.works.map(({ id }) => id);
    this.subscriptions.push(
      this.workService.getStudentWorks(this.subjectId, workIds).subscribe((response: ResponseModel<StudentWork[]>) => {
        this.studentWorks = response.payload;
        this.createRatingCells();
      })
    );
  }
  updateStudentWorks() {
    this.subscriptions.push(
      this.workService.updateStudentWorks(this.studentWorks).subscribe(() => {
      })
    );
  }

}
