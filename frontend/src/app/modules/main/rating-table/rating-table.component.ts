import { Component, OnInit, Input } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from 'src/app/core/services/student.service';
import { StudentWork } from 'src/app/shared/models/student-work.model';
@Component({
  selector: 'app-rating-table',
  templateUrl: './rating-table.component.html',
  styleUrls: ['./rating-table.component.scss']
})
export class RatingTableComponent extends BaseComponent implements OnInit {
  lects: Work[] = [];
  practs: Work[] = [];
  labs: Work[] = [];
  subject: SubjectDto;
  works: Work[];
  students: Student[];
  subjectId: number;
  studentCriteria: StudentCriteria[] = [];
  studentWorks: StudentWork[] = [];
  editField: string;
  data = [];
  loaded = false;
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
    this.getWorks();
  }

  getWorks() {
    this.subscriptions.push(
      this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
        this.works = response.payload;
        this.countWorks();
        this.getStudents();
      })
    );
  }
  countWorks() {
    this.works.forEach(element => {
     switch (element.workTypeId) {
       case 1:
         this.lects.push(element);
         break;
      case 2:
          this.practs.push(element);
          break;
      case 3:
          this.labs.push(element);
          break;
       default:
         break;
     }
    });
  }
  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        this.loaded = true;
        this.getStudentWorks();
        this.getStudentCriteria();
      })
    );
  }

  getStudentCriteria() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.studentService.getStudentCriterias(studentIds).subscribe((response: ResponseModel<StudentCriteria[]>) => {
        this.studentCriteria = response.payload;
      })
    );
  }
  updateStudentCriteria() {
    this.subscriptions.push(
      this.studentService.updateStudentCriterias(this.studentCriteria).subscribe()
    );
  }

  getStudentWorks() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.studentService.getStudentWorks(studentIds).subscribe((response: ResponseModel<StudentWork[]>) => {
        this.studentWorks = response.payload;
        this.createRatingCells();
      })
    );
  }
  updateStudentWorks() {
    this.subscriptions.push(
      this.studentService.updateStudentWorks(this.studentWorks).subscribe(() => {
        this.router.navigate(['/subjects']);
      })
    );
  }

  updateWorkPoints(studentWork: StudentWork, event: any) {
    const editField = event.target.textContent;
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

  changeValue(studentWork: StudentWork, event: any) {
    this.editField = event.target.textContent;
  }

  createRatingCells() {
    let row = [];
    this.data = [];
    this.students.forEach(student => {
      let rowSum = 0;
      this.works.forEach(work => {
        const sp = this.studentWorks.find(x => x.workId === work.id && x.studentId === student.id);
        if (sp) {
          row.push(sp);
          rowSum += +sp.sumOfPoints;
        } else {
          const studentWork = new StudentWork();
          studentWork.workId = work.id;
          studentWork.studentId = student.id;
          row.push(studentWork);
        }
      });
      this.data.push({ works: row, currentStudent: student, sumOfPoints: rowSum});
      row = [];
    });
  }

  updateRating() {
    this.updateStudentWorks();
  }
}
