import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { StudentService } from 'src/app/core/services/student.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/shared/models/student.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Totals } from 'src/app/shared/models/totals.model';
import { ExamResult } from 'src/app/shared/models/exam-result.model';
import { GeneralService } from 'src/app/core/services/general.service';
import { Mode } from 'src/app/shared/models/mode.model';
import { NotifierService } from 'angular-notifier';
import { RoleService } from 'src/app/core/services/role.service';
import { StatusEnum } from 'src/app/shared/enums/status.enum';
import { Status } from 'src/app/shared/models/status.model';
@Component({
  selector: 'app-exam-table',
  templateUrl: './exam-table.component.html',
  styleUrls: ['./exam-table.component.scss']
})
export class ExamTableComponent extends BaseComponent implements OnInit {
  modes = [{id: 1 , name: 'Текущий учет'}, {id: 2, name: 'Экзамен'}, {id: 3, name: 'Пересдача'}];
  subjectId: number;
  mode: Mode = new Mode();
  students: Student[] = [];
  editField: string;
  data = [];
  totals: Totals[] = [];
  additionalTotals: Totals[] = [];
  examResults: ExamResult[] = [];
  hasAccess = false;
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
  constructor(
    private readonly subjectService: SubjectService,
    private readonly notifierService: NotifierService,
    private readonly generalService: GeneralService,
    private readonly roleService: RoleService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.initializeTable();
    this.getCurrentMode();
    this.checkStatus();
  }

  initializeTable() {
    this.getStudents();
  }

  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        this.getTotals();
      })
    );
  }
  updateWorkPoints(examResult: ExamResult, type: string, event: any) {
    const editField = event.target.textContent;
    if ( editField > 40 /*|| !this.regex.test(editField)*/) {
      event.target.classList.add('off-limits');
      return;
    } else {
      event.target.classList.remove('off-limits');
    }
    switch (type) {
      case 'first':
        examResult.points = +editField;
        break;
      case 'second':
        examResult.secondPassPoints = +editField;
        break;
      case 'third':
        examResult.thirdPassPoints = +editField;
        break;
      default:
        break;
    }
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
      this.data.push({totals: total,
                      additional: atotal,
                      currentStudent: student,
                      examResult: examRes,
                      sumOfPoints: rowSum > 100 ? 100 : rowSum});
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
    if (!this.hasAccess) {
      return false;
    }
    if (totals >= 38) {
      return true;
    } else {
      return false;
    }
  }
  addFull(examResult: ExamResult) {
    examResult.points === 40 ? examResult.points = 0 :  examResult.points = 40;
  }
  addZero(examResult: ExamResult) {
    examResult.points = 0;
  }
  addZeroSecond(examResult: ExamResult) {
    examResult.secondPassPoints = 0;
  }
  addZeroThird(examResult: ExamResult) {
    examResult.thirdPassPoints = 0;
  }
  getCurrentMode() {
    this.subscriptions.push(
      this.generalService.getCurrentMode().subscribe((response: ResponseModel<number>) => {
        this.mode = this.modes.find(x => x.id === response.payload);
        if (this.mode.id === 1) {
          this.notifierService.notify('error', 'Страница недоступна');
          this.router.navigate(['/dashboard']);
        }
      })
    );
  }
  checkStatus() {
    const employeeId = this.roleService.getCurrentUserEmployeeId();
    if (!employeeId) {
      this.notifierService.notify('error', 'У вас нет прав на редактирование');
    }
    this.subjectService.getRoles(this.subjectId, employeeId).subscribe((response: ResponseModel<Status[]>) => {
      response.payload.forEach(element => {
        if (element.name === StatusEnum.Main || element.name === StatusEnum.Examiner) {
          this.hasAccess = true;
        }
      });
    });
  }
 }
