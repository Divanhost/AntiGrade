import { Component, OnInit } from '@angular/core';
import { Semester } from 'src/app/shared/models/semester.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GeneralService } from 'src/app/core/services/general.service';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Component({
  selector: 'app-semester',
  templateUrl: './semester.component.html',
  styleUrls: ['./semester.component.scss']
})
export class SemesterComponent extends BaseComponent implements OnInit {

  semester: Semester = new Semester();
  prevSemester: Semester = new Semester();
  semNum: number;
  year: number;
  constructor(
    private readonly generalService: GeneralService,
    private readonly notifierService: NotifierService,
    private router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.getLastSemester();
  }
  getLastSemester() {
    this.subscriptions.push(
      this.generalService.getLastSemester().subscribe((response: ResponseModel<Semester>) => {
        this.prevSemester = response.payload;
        this.semNum = this.getNextSemesterNum();
        this.year = !this.prevSemester.isFirstHalf ? this.prevSemester.year + 1 : this.prevSemester.year;
      })
    );
  }
  updateSemester() {
    this.semester.isFirstHalf = this.semNum === 1;
    this.semester.year = this.year;
    this.semester.name = !this.semester.isFirstHalf ?
        `${this.semNum} семестр ${this.year - 1}/${this.year}` : `${this.semNum} семестр ${this.year}/${this.year + 1}`;
    if (this.compareSemesters()) {
      this.subscriptions.push(
        this.generalService.createSemester(this.semester).subscribe((response: ResponseModel<boolean>) => {
          this.notifierService.notify('success', 'Семестр успешно добавлен');
          this.router.navigate(['/dashboard']);
        })
      );
    } else {
      this.notifierService.notify('error', 'Невозможно добавить семестр');
    }
  }
  compareSemesters(): boolean {
    if (this.prevSemester.isFirstHalf && !this.semester.isFirstHalf) {
      if (this.prevSemester.year !== this.semester.year) {
        return true;
      } else {
        return false;
      }
    } else if (!this.prevSemester.isFirstHalf && this.semester.isFirstHalf) {
      if (this.prevSemester.year === this.semester.year) {
        return true;
      } else {
        return false;
      }
    }
  }
  getNextSemesterNum() {
    return  2- +this.prevSemester.isFirstHalf;
  }
}
