import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { StudentService } from 'src/app/core/services/student.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/shared/models/student.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Totals } from 'src/app/shared/models/totals.model';
import { ExamResult } from 'src/app/shared/models/exam-result.model';
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
  data = [];
  totals: Totals[] = [];
  additionalTotals: Totals[] = [];
  examResults: ExamResult[] = [];
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

  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        this.getTotals();
      })
    );
  }
  updateWorkPoints(examResult: ExamResult, event: any) {
    const editField = event.target.textContent;
    if ( editField > 40 /*|| !this.regex.test(editField)*/) {
      event.target.classList.add('off-limits');
      return;
    } else {
      event.target.classList.remove('off-limits');
    }
    examResult.points = +editField;
    if ( examResult.points.toString() !== '' && examResult.points !== null) {
      const hasWork = this.examResults.find(x => x.studentId === examResult.studentId);
      if (!hasWork) {
        this.examResults.push(examResult);
      }
    } else {
      examResult.points = 0;
    }
    this.createRatingCells();
  }

  changeValue(studentWork, event: any) {
    this.editField = event.target.textContent;
  }
  getTotals() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getSubjectTotals(this.subjectId, studentIds).subscribe((response: ResponseModel<Totals[]>) => {
        this.totals = response.payload;
        this.getAdditionals();
      })
    );
  }
  getAdditionals() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getSubjectAdditionalTotals(this.subjectId, studentIds).subscribe((response: ResponseModel<Totals[]>) => {
        this.additionalTotals = response.payload;
        this.getExamResults();
      })
    );
  }
  createRatingCells() {
    let row = [];
    this.data = [];
    this.students.forEach(student => {
      let rowSum = 0;
      const total = this.totals.find(x => x.studentId === student.id).totals;
      const atotal = this.additionalTotals.find(x => x.studentId === student.id).totals;
      let examRes = this.examResults.find(x => x.studentId === student.id);
      rowSum += total;
      rowSum += atotal;
      if (!examRes) {
        examRes = new ExamResult();
        examRes.studentId = student.id;
        examRes.subjectId = this.subjectId;
      } else {
        rowSum += +examRes.points;
      }
      this.data.push({ totals: total, additional: atotal, currentStudent: student, examResult: examRes, sumOfPoints: rowSum});
      row = [];
    });
  }
  getExamResults() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getExamResults(this.subjectId, studentIds).subscribe((response: ResponseModel<ExamResult[]>) => {
        this.examResults = response.payload;
        console.log(this.examResults);
        this.createRatingCells();
      })
    );
  }
  updateExamResults() {
    console.log(this.examResults);
    this.subscriptions.push(
      this.subjectService.updateExamResults(this.examResults).subscribe((response: ResponseModel<boolean>) => {
      })
    );
  }
  canEdit(totals: number) {
    if (totals >= 38) {
      return true;
    } else {
      return false;
    }
  }
 }
