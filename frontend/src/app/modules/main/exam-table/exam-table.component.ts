import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { StudentService } from 'src/app/core/services/student.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/shared/models/student.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
@Component({
  selector: 'app-exam-table',
  templateUrl: './exam-table.component.html',
  styleUrls: ['./exam-table.component.scss']
})
export class ExamTableComponent extends BaseComponent implements OnInit {
  subjectId: number;
  mode = 1;
  students: Student[] = [];
  editField: string;
  constructor(
    private readonly subjectService: SubjectService,
    private readonly studentService: StudentService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.initializeTable();
  }

  initializeTable() {
    this.getStudents();
    // this.getWorks();
  }

  // getWorks() {
  //   this.subscriptions.push(
  //     this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
  //       this.works = response.payload;
  //       console.log(this.works);
  //       this.countWorks();
  //       this.getStudents();
  //     })
  //   );
  // }
  // countWorks() {
  //   this.works.forEach(element => {
  //    switch (element.workTypeId) {
  //      case 1:
  //        this.lects += 1;
  //        break;
  //     case 2:
  //         this.practs += 1;
  //         break;
  //     case 3:
  //         this.labs += 1;
  //         break;
  //      default:
  //        break;
  //    }
  //   });
  // }
  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        console.log(this.students);
        // this.getStudentWorks();
        // this.getStudentCriteria();
      })
    );
  }

  // getStudentCriteria() {
  //   const studentIds = this.students.map(({ id }) => id);
  //   this.subscriptions.push(
  //     this.studentService.getStudentCriterias(studentIds).subscribe((response: ResponseModel<StudentCriteria[]>) => {
  //       this.studentCriteria = response.payload;
  //     })
  //   );
  // }
  // updateStudentCriteria() {
  //   this.subscriptions.push(
  //     this.studentService.updateStudentCriterias(this.studentCriteria).subscribe()
  //   );
  // }

  // getStudentWorks() {
  //   const studentIds = this.students.map(({ id }) => id);
  //   this.subscriptions.push(
  //     this.studentService.getStudentWorks(studentIds).subscribe((response: ResponseModel<StudentWork[]>) => {
  //       this.studentWorks = response.payload;
  //       this.createRatingCells();
  //     })
  //   );
  // }
  // updateStudentWorks() {
  //   this.subscriptions.push(
  //     this.studentService.updateStudentWorks(this.studentWorks).subscribe(() => {
  //       this.router.navigate(['/subjects']);
  //     })
  //   );
  // }

  updateWorkPoints(studentWork, event: any) {
    // const editField = event.target.textContent;
    // studentWork.sumOfPoints = editField;
    // if ( studentWork.sumOfPoints.toString() !== '' && studentWork.sumOfPoints !== null) {
    //   const hasWork = this.studentWorks.find(x => x.workId === studentWork.workId && x.studentId === studentWork.studentId);
    //   if (!hasWork) {
    //     this.studentWorks.push(studentWork);
    //   }
    // } else {
    //   studentWork.sumOfPoints = 0;
    // }
    // this.createRatingCells();
  }

  changeValue(studentWork, event: any) {
    this.editField = event.target.textContent;
  }

  // createRatingCells() {
  //   let row = [];
  //   this.data = [];
  //   this.students.forEach(student => {
  //     let rowSum = 0;
  //     this.works.forEach(work => {
  //       const sp = this.studentWorks.find(x => x.workId === work.id && x.studentId === student.id);
  //       if (sp) {
  //         row.push(sp);
  //         rowSum += +sp.sumOfPoints;
  //       } else {
  //         const studentWork = new StudentWork();
  //         studentWork.workId = work.id;
  //         studentWork.studentId = student.id;
  //         row.push(studentWork);
  //       }
  //     });
  //     this.data.push({ works: row, currentStudent: student, sumOfPoints: rowSum});
  //     row = [];
  //   });
  // }

  // updateRating() {
  //   this.updateStudentWorks();
  // }

}
