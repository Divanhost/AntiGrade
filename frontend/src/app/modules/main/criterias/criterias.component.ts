import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Student } from 'src/app/shared/models/student.model';
import { Criteria } from 'src/app/shared/models/criteria.model';
import { BaseComponent } from 'src/app/shared/classes';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { WorkService } from 'src/app/core/services/work.service';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.scss']
})
export class CriteriasComponent extends BaseComponent implements OnInit {
  @Input() workId: number;
  @Input() criterias: Criteria[] = [];
  @Input() students: Student[] = [];
  studentCriterias: StudentCriteria[] = [];
  data = [];
  constructor(private readonly workService: WorkService) {
    super();
  }

  ngOnInit(): void {
   console.log(this.workId);
   console.log(this.criterias);
   console.log(this.students);
   this.getStudentCriteria();
  }
  updateCriteriaPoints(studentCriteria: StudentCriteria, event: any) {
    const editField = event.target.textContent;
    studentCriteria.points = +editField;
    if (studentCriteria.points.toString() !== '' && studentCriteria.points !== null) {
      const hasCriteria = this.studentCriterias.find(x => x.criteriaId === studentCriteria.criteriaId
        && x.studentId === studentCriteria.studentId);
      if (!hasCriteria) {
        this.studentCriterias.push(studentCriteria);
      }
    } else {
      studentCriteria.points = 0;
    }
    this.createRatingCells();
  }
  createRatingCells() {
    let row = [];
    this.data = [];
    this.students.forEach(student => {
      let rowSum = 0;
      this.criterias.forEach(criteria => {
        const sp = this.studentCriterias.find(x => x.criteriaId === criteria.id && x.studentId === student.id);
        if (sp) {
          row.push(sp);
          rowSum += +sp.points;
        } else {
          const studentCriteria = new StudentCriteria();
          studentCriteria.criteriaId = criteria.id;
          studentCriteria.studentId = student.id;
          row.push(studentCriteria);
        }
      });
      this.data.push({criterias: row, currentStudent: student, sumOfPoints: rowSum });
      console.log(this.data);
      row = [];
    });
  }

  getStudentCriteria() {
    this.subscriptions.push(
      this.workService.getStudentCriterias(this.workId).subscribe((response: ResponseModel<StudentCriteria[]>) => {
        this.studentCriterias = response.payload;
        this.createRatingCells();
      })
    );
  }
  updateStudentCriteria() {
    this.subscriptions.push(
      this.workService.updateStudentCriterias(this.studentCriterias).subscribe()
    );
  }
}
