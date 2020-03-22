import { Component, OnInit, Input } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';
import { StudentService } from 'src/app/core/services/student.service';
import { StudentWork } from 'src/app/shared/models/student-work.model';
@Component({
  selector: 'app-rating-table',
  templateUrl: './rating-table.component.html',
  styleUrls: ['./rating-table.component.scss']
})
export class RatingTableComponent extends BaseComponent implements OnInit {

  subject: SubjectDto;
  works: Work[];
  students: Student[];
  subjectId: number;
  studentCriteria: StudentCriteria[] = [];
  studentWorks: StudentWork[] = [];
  editField: string;
  data = [];
  constructor(
    private readonly subjectService: SubjectService,
    private readonly studentService: StudentService,
    private readonly route: ActivatedRoute) {
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
        this.getStudents();
      })
    );
  }
  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
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
        console.log(this.data);
      })
    );
  }
  updateStudentWorks() {
    this.subscriptions.push(
      this.studentService.updateStudentWorks(this.studentWorks).subscribe(() => {
      })
    );
  }

  updateWorkPoints(studentWork: StudentWork, event: any) {
    const editField = event.target.textContent;

    studentWork.SumOfPoints = editField;
    if ( studentWork.SumOfPoints.toString() !== '' && studentWork.SumOfPoints !== null) {
      const hasWork = this.studentWorks.find(x => x.workId === studentWork.workId && x.studentId === studentWork.studentId);
      if (!hasWork) {
        this.studentWorks.push(studentWork);
      }
    } else {
      studentWork.SumOfPoints = 0;
    }
  }

  changeValue(studentWork: StudentWork, event: any) {
    this.editField = event.target.textContent;
  }

  createRatingCells() {
    let row = [];
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
      this.data.push({ works: row, currentStudent: student });
      row = [];
    });
  }

  updateRating() {
    this.updateStudentWorks();
  }
}
