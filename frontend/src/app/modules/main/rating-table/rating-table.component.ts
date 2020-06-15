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
import { RoleService } from 'src/app/core/services/role.service';
import { WorkService } from 'src/app/core/services/work.service';
import { GeneralService } from 'src/app/core/services/general.service';
import { Mode } from 'src/app/shared/models/mode.model';
import { Status } from 'src/app/shared/models/status.model';
import { StatusEnum } from 'src/app/shared/enums/status.enum';
import { NotifierService } from 'angular-notifier';
import * as jspdf from 'jspdf';
import * as html2canvas from 'html2canvas';
import * as es6printJs from 'print-js';
import { ExcelService } from 'src/app/core/services/excel.service';
import { Totals } from 'src/app/shared/models/totals.model';
import { ExamResult } from 'src/app/shared/models/exam-result.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { Semester } from 'src/app/shared/models/semester.model';
@Component({
  selector: 'app-rating-table',
  templateUrl: './rating-table.component.html',
  styleUrls: ['./rating-table.component.scss']
})
export class RatingTableComponent extends BaseComponent implements OnInit {
  modes = [{id: 1 , name: 'Текущий учет'}, {id: 2, name: 'Экзамен'}, {id: 3, name: 'Пересдача'}];
  additionalPageMode: boolean;
  lects: Work[] = [];
  disableLects = true;
  practs: Work[] = [];
  disablePracts = true;
  labs: Work[] = [];
  disableLabs = true;
  subject: SubjectDto;
  works: Work[];
  mode: Mode = new Mode();
  students: Student[] = [];
  subjectId: number;
  studentCriteria: StudentCriteria[] = [];
  studentWorks: StudentWork[] = [];
  bonusWorkPoints: StudentWork[] = [];
  bonusWork: Work = new Work();
  editField: string;
  semester: Semester = new Semester();
  examType: ExamType = new ExamType();
  additionalTotals: Totals[] = [];
  examResults: ExamResult[] = [];
  data = [];
  totals = [];
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
    private readonly studentService: StudentService,
    private readonly workService: WorkService,
    private readonly excelSrv: ExcelService,
    private readonly roleService: RoleService,
    private readonly generalService: GeneralService,
    private readonly notifierService: NotifierService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) {
    super();
    this.router.routeReuseStrategy.shouldReuseRoute = function() {
      return false;
    };
    this.additionalPageMode = this.route.snapshot.url[2].toString() === 'additional';
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.checkStatus();
    this.initializeTable();
    
  }

  initializeTable() {
    this.getWorks();
  }
  getExamType() {
    this.subscriptions.push(
      this.subjectService.getExamType(this.subjectId).subscribe((response: ResponseModel<ExamType>) => {
        this.examType = response.payload;
      })
    );
  }

  getWorks() {
    this.subscriptions.push(
      this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
        this.works = response.payload.filter(x => x.workTypeId !== 4);
        this.bonusWork = response.payload.find(x => x.workTypeId === 4);
        this.getExamType();
        this.countWorks();
        if (!this.additionalPageMode) {
          this.getStudents();
        } else {
          this.getStudentsWithoutExam();
        }
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
        this.getCurrentMode();
      })
    );
  }
  getStudentsWithoutExam() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudentsWithoutExam(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        this.getCurrentMode();
      })
    );
  }
  getStudentWorks() {
    this.subscriptions.push(
      this.workService.getStudentWorks(this.subjectId).subscribe((response: ResponseModel<StudentWork[]>) => {
        if(this.bonusWork) {
          this.studentWorks = response.payload.filter(x => x.workId !== this.bonusWork.id);
          this.bonusWorkPoints = response.payload.filter(x => x.workId === this.bonusWork.id);
        } else {
          this.studentWorks = response.payload;
          this.bonusWorkPoints = []
        }
        this.loaded = true;
        this.updateSum();
      })
    );
  }

  updateStudentWorks() {
    if (this.mode.id === 3) {
      this.studentWorks.forEach(element => {
        element.isAdditional = true;
      });
    }
    this.studentWorks = [...this.studentWorks, ...this.bonusWorkPoints];
    this.subscriptions.push(
      this.studentService.updateStudentWorks(this.studentWorks).subscribe(() => {
        this.notifierService.notify('success', 'Изменения сохранены');
        this.router.navigate(['/subjects']);
      })
    );
  }

  changeValue(studentWork: StudentWork, event: any) {
    this.editField = event.target.textContent;
  }

  checkStatus() {
    const employeeId = this.roleService.getCurrentUserEmployeeId();
    if (!employeeId) {
      this.notifierService.notify('error', 'У вас нет прав на редактирование');
    }
    this.subjectService.getRoles(this.subjectId, employeeId).subscribe((response: ResponseModel<Status[]>) => {
      response.payload.forEach(element => {
        switch (element.name) {
          case StatusEnum.Main:
            this.disableLabs = false;
            this.disablePracts = false;
            this.disableLects = false;
            break;
          case StatusEnum.Lecturer:
            this.disableLabs = false;
            break;
          case StatusEnum.Practice:
            this.disablePracts = false;
            break;
          case StatusEnum.Lab:
            this.disableLabs = false;
            break;
          default:
            break;
        }
      });
    });
  }
  updateData(studentWork: StudentWork) {
    const sw = this.studentWorks.find(x => x.studentId === studentWork.studentId && x.workId === studentWork.workId);
    if (sw) {
      sw.sumOfPoints = studentWork.sumOfPoints;
    } else {
      this.studentWorks.push(studentWork);
    }
    this.updateSum();
  }
  updateSum() {
    this.students.forEach(student => {
      let studentTotals = 0;
      this.studentWorks.forEach((item) => {
        if (item.studentId === student.id) {
          studentTotals += item.sumOfPoints;
        }
      });
      const st = this.totals.find(x => x.studentId === student.id);
      if (st) {
        st.totals = studentTotals;
      } else {
        this.totals.push({studentId: student.id, totals: studentTotals});
      }
    });
  }

  getCurrentMode() {
    this.subscriptions.push(
      this.generalService.getCurrentMode().subscribe((response: ResponseModel<number>) => {
        this.mode = this.modes.find(x => x.id === response.payload);
        if (this.mode.id !== 1 && !this.additionalPageMode) {
          this.getExamResults();
        }
        if (!this.additionalPageMode) {
          this.getStudentWorks();
          if (this.isAdditionalMode) {
            this.getAdditionals();
          }
        } else {
          this.fillAdditional();
        }
      })
    );
  }
  fillAdditional() {
    this.getAdditionalStudentWorks();
    this.getAdditionals();
    this.getExamResults();
  }
  getAdditionalStudentWorks() {
    this.subscriptions.push(
      this.workService.getAdditionalStudentWorks(this.subjectId).subscribe((response: ResponseModel<StudentWork[]>) => {
        this.studentWorks = response.payload;
        this.loaded = true;
        this.updateSum();
      })
    );
  }

  getAdditionals() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getSubjectAdditionalTotals(this.subjectId, studentIds).subscribe((response: ResponseModel<Totals[]>) => {
        this.additionalTotals = response.payload;
      })
    );
  }

  getExamResults() {
    const studentIds = this.students.map(({ id }) => id);
    this.subscriptions.push(
      this.subjectService.getExamResults(this.subjectId, studentIds).subscribe((response: ResponseModel<ExamResult[]>) => {
        this.examResults = response.payload;
      })
    );
  }

  exportData(tableId: string) {
    this.excelSrv.exportToFile('contacts', tableId).subscribe();
  }
  getCurrentRating() {

  }
  getAdditionalRating() {

  }
  getSum(index) {
    let sum = this.totals[index].totals;
    if (this.additionalTotals.length) {
      sum += this.additionalTotals[index].totals;
      if(sum > 38) {
        sum = 38;
      }
    }
    if (this.examResults.length && this.examType.id === 1) {
      sum += this.getExamPoints(this.examResults[index]);
    }
    if (isNaN(sum)) {
      debugger
      return 0;
    }
    if (this.examResults.length) {
      if (this.examResults[index].isFailed) {
        return sum > 60 ? 60 : sum;
      } else {
        return sum > 100 ? 100 : sum;
      }
    } else {
      return sum > 100 ? 100 : sum;
    }
  }
  getStudentAdditional(i) {
    if(this.additionalTotals[i].totals + this.totals[i].totals > 38 && this.totals[i].totals < 38) {
      this.additionalTotals[i].totals = 38 - this.totals[i].totals ;
    }
    return this.additionalTotals[i].totals;
  }
  getExamPoints(examResult: ExamResult) {
    if (examResult.thirdPassPoints) {
      return examResult.thirdPassPoints;
    }
    if (examResult.secondPassPoints) {
      return examResult.secondPassPoints;
    }
    if (examResult.points) {
      return examResult.points;
    }
    return 0;
  }
  getGrade(index: number) {
    const points = this.getSum(index);
    if (this.examType.id === 3) {
      if (points >= 60 ) {
        return 'Зачет';
      } else {
        return 'Незачет';
      }
    } else {
      if (points >= 85 ) { return 'Отлично'; }
      if (points >= 71 ) { return 'Хорошо'; }
      if (points >= 60 ) { return 'Удовл.'; }
      return 'Неуд.';
    }
  }
  getStudentBonus(student: Student) {
    const wp = this.bonusWorkPoints.find(x => x.studentId === student.id);
    if (wp) {
      return wp.sumOfPoints;
    } else {
      return 0;
    }
  }
  updateBonusPoints(i: number, event: any) {
    const editField = event.target.textContent.replace(/\s/g, '');
    const student = this.students[i];
    const studentBonuses = new StudentWork();
    studentBonuses.isAdditional = false;
    studentBonuses.studentId = student.id;
    studentBonuses.workId = this.bonusWork.id;
    studentBonuses.sumOfPoints = +editField;
    const hasBonuses = this.bonusWorkPoints.find(x => x.studentId === student.id);
    const numberReSnippet = '(?:NaN|-?(?:(?:\\d+|\\d*\\.\\d+)(?:[E|e][+|-]?\\d+)?|Infinity))';
    const matchOnlyNumberRe = new RegExp('^(' + numberReSnippet + ')$');
    if ( +editField > 10 || !matchOnlyNumberRe.test(editField)) {
      if (!editField) {
        return;
      }
      event.target.textContent = 0;
      this.notifierService.notify('error', 'Неверно введены баллы');
      return;
    }
    if (!hasBonuses) {
      this.bonusWorkPoints.push(studentBonuses);
    }
  }


  // print() {
  //   const data = document.getElementById('contentToPrint');
  //   html2canvas(data).then(canvas => {
  //   const imgWidth = 208;
  //   const imgHeight = canvas.height * imgWidth / canvas.width;
  //   const contentDataURL = canvas.toDataURL('image/png');
  //   const pdf = new jspdf('p', 'mm', 'a4');
  //   const position = 0;
  //   pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
  //   es6printJs({
  //   printable: contentDataURL,
  //   type: 'image',
  //   });
  //   });
  //   }

}
