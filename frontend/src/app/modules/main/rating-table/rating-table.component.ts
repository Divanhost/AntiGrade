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
import * as XLSX from 'xlsx';
import { ExcelService } from 'src/app/core/services/excel.service';
import { Totals } from 'src/app/shared/models/totals.model';
import { ExamResult } from 'src/app/shared/models/exam-result.model';
@Component({
  selector: 'app-rating-table',
  templateUrl: './rating-table.component.html',
  styleUrls: ['./rating-table.component.scss']
})
export class RatingTableComponent extends BaseComponent implements OnInit {
  modes = [{id: 1 , name: 'Текущий учет'}, {id: 2, name: 'Экзамен'}, {id: 3, name: 'Пересдача'}];
  lects: Work[] = [];
  disableLects = true;
  practs: Work[] = [];
  disablePracts = true;
  labs: Work[] = [];
  disableLabs = true;
  subject: SubjectDto;
  works: Work[];
  mode: Mode = new Mode();
  students: Student[];
  subjectId: number;
  studentCriteria: StudentCriteria[] = [];
  studentWorks: StudentWork[] = [];
  editField: string;
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
    // this.router.routeReuseStrategy.shouldReuseRoute = function() {
    //   return false;
    // };
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

  getWorks() {
    this.subscriptions.push(
      this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
        this.works = response.payload;
        console.log(this.works);
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
        this.getCurrentMode();
      })
    );
  }
  getStudentWorks() {
    this.subscriptions.push(
      this.workService.getStudentWorks(this.subjectId).subscribe((response: ResponseModel<StudentWork[]>) => {
        this.studentWorks = response.payload;
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
      console.log(response.payload);
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
        if (this.mode.id !== 1) {
          this.getExamResults();
        }
        if (this.mode.id !== 3) {
          this.getStudentWorks();
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
}
