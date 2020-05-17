import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/shared/models/course.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GeneralService } from 'src/app/core/services/general.service';
import { NotifierService } from 'angular-notifier';
import { Router, ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Component({
  selector: 'app-add-edit-course',
  templateUrl: './add-edit-course.component.html',
  styleUrls: ['./add-edit-course.component.scss']
})
export class AddEditCourseComponent extends BaseComponent implements OnInit {

  courseId: number;
  course: Course = new Course();
  isCreate: boolean;
  constructor(private readonly generalService: GeneralService,
              private readonly notifierService: NotifierService,
              private readonly router: Router,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.courseId = params.id)
    );
    this.isCreate = this.courseId === undefined;
  }
  ngOnInit(): void {
    if (!this.isCreate) {
      this.getCourse();
    }
  }
  getCourse() {
    this.subscriptions.push(
      this.generalService.getCourse(this.courseId).subscribe((response: ResponseModel<Course>) => {
        this.course = response.payload;
      })
    );
  }

  deleteCourse() {
    this.subscriptions.push(
      this.generalService.deleteCourse(this.courseId).subscribe((response: ResponseModel<boolean>) => {
      })
    );
  }
  onSubmit() {
    if (this.course.name === null || this.course.name === '' || this.course.name === undefined) {
      this.notifierService.notify('error', 'Нельзя создать направление');
      return;
    }
    if (this.isCreate) {
      this.subscriptions.push(
        this.generalService.createCourse(this.course).subscribe(() => {
          this.router.navigate(['/courses']);
        })
      );
    } else {
      this.subscriptions.push(
        this.generalService.updateCourse(this.course).subscribe(() => {
          this.router.navigate(['/courses']);
        })
      );
    }
  }

}
