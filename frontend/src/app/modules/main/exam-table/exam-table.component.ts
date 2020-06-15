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
import { SubjectExamStatus } from 'src/app/shared/models/subject-exam-status.model';
@Component({
  selector: 'app-exam-table',
  templateUrl: './exam-table.component.html',
  styleUrls: ['./exam-table.component.scss']
})
export class ExamTableComponent extends BaseComponent implements OnInit {
  modes = [{ id: 1, name: 'Текущий учет' }, { id: 2, name: 'Экзамен' }, { id: 3, name: 'Пересдача' }];
  subjectId: number;
  mode: Mode = new Mode();
  students: Student[] = [];
  editField: string;
  data = [];
  totals: Totals[] = [];
  additionalTotals: Totals[] = [];
  examResults: ExamResult[] = [];
  examStatus: SubjectExamStatus = new SubjectExamStatus();
  hasAccess = false;
  loaded = false;
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
    this.getExamStatus();
    this.getStudents();
  }
  getExamStatus() {
    this.subscriptions.push(
      this.subjectService.getExamStatus(this.subjectId).subscribe((response: ResponseModel<SubjectExamStatus>) => {
        this.examStatus = response.payload;
      })
    );
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
    const editField = event.target.textContent.replace(/\s/g, '');
    const numberReSnippet = '(?:NaN|-?(?:(?:\\d+|\\d*\\.\\d+)(?:[E|e][+|-]?\\d+)?|Infinity))';
    const matchOnlyNumberRe = new RegExp('^(' + numberReSnippet + ')$');
    debugger
    if (!editField || +editField === 0 || editField === '') {
      return;
    }
    if (+editField > 40 || !matchOnlyNumberRe.test(editField)) {
      event.target.textContent = 0;
      this.notifierService.notify('error', 'Неверно введены баллы');
      return;
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
    if (examResult.points.toString() !== '' && examResult.points !== null) {
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
      let atotal = this.additionalTotals.find(x => x.studentId === student.id).totals;
      if (total + atotal >= 38 && total < 38) {
        atotal = 38 - total;
      }
      let examRes = this.examResults.find(x => x.studentId === student.id);
      rowSum += total;
      rowSum += atotal;
      if (!examRes) {
        examRes = new ExamResult();
        examRes.studentId = student.id;
        examRes.subjectId = this.subjectId;
      } else {
        if (examRes.thirdPassPoints) {
          rowSum += examRes.thirdPassPoints;
          rowSum = rowSum > 60 ? 60 : rowSum;
        } else if (examRes.secondPassPoints) {
          rowSum += examRes.secondPassPoints;
          rowSum = rowSum > 60 ? 60 : rowSum;
        } else if (examRes.points) {
          rowSum += examRes.points;
          rowSum = rowSum > 100 ? 100 : rowSum;
        }
      }
      this.data.push({
        totals: total,
        additional: atotal,
        currentStudent: student,
        examResult: examRes,
        sumOfPoints: rowSum
      });
      row = [];
    });
    this.loaded = true;
  }
  getExamResults() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getExamResults(this.subjectId, studentIds).subscribe((response: ResponseModel<ExamResult[]>) => {
        this.examResults = response.payload;
        this.createRatingCells();
      })
    );
  }
  updateExamResults() {
    this.subscriptions.push(
      this.subjectService.updateExamResults(this.examResults).subscribe((response: ResponseModel<boolean>) => {
        this.notifierService.notify('success', 'Изменения сохранены');
      })
    );
  }
  canEdit(row) {
    const totals = row.totals;
    const additionals = row.additional;
    if (!this.hasAccess) {
      return false;
    }
    if (totals > 37) {
      return true;
    } else {
      return false;
    }
  }
  canEditFirst(row) { 
      const totals = row.totals;
      const additionals = row.additional;
      if (!this.hasAccess) {
        return false;
      }
      if (row.examResult) {
        if (row.examResult.points > 21) {
          return false;
        }
      }
      if (totals + additionals >= 38) {
        return true;
      }

      return false;
  }
  canEditSecond(row) {
    const totals = row.totals;
    const additionals = row.additional;
    if (!this.hasAccess) {
      return false;
    }
    if (row.examResult) {
      if (row.examResult.secondPassPoints || row.examResult.points) {
        return false;
      }
    }

    if (totals + additionals >= 38) {
      return true;
    }

    return false;
}
  addFull(examResult: ExamResult) {
    examResult.points === 40 ? examResult.points = 0 : examResult.points = 40;
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
  startExam() {
    this.examStatus.isExamStarted = true;
    this.updateExamStatus();
    this.notifierService.notify('success', 'Экзамен начался');
  }
  closeExam() {
    this.examStatus.isExamClosed = true;
    this.updateExamStatus();
    this.updateExamResults();
    this.notifierService.notify('success', 'Экзамен завершен');
  }
  startFirstRetake() {
    this.examStatus.isFirstRetakeStarted = true;
    this.updateExamStatus();
    this.notifierService.notify('success', 'Пересдача начата');
  }
  closeFirstRetake() {
    this.examStatus.isFirstRetakeClosed = true;
    this.updateExamStatus();
    this.updateExamResults();
    this.notifierService.notify('success', 'Пересдача завершена');
  }
  startSecondRetake() {
    this.examStatus.isSecondRetakeStarted = true;
    this.updateExamStatus();
    this.notifierService.notify('success', 'Пересдача начата');
  }
  closeSecondRetake() {
    this.examStatus.isSecondRetakeClosed = true;
    this.updateExamStatus();
    this.updateExamResults();
    this.notifierService.notify('success', 'Пересдача завершена');
  }
  updateExamStatus() {
    this.subscriptions.push(
      this.subjectService.updateExamStatus(this.subjectId, this.examStatus).subscribe((response: ResponseModel<boolean>) => {
      })
    );
  }
}
