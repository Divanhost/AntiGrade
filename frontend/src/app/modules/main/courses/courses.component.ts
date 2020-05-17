import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/shared/models/course.model';
import { BaseComponent } from 'src/app/shared/classes';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { GeneralService } from 'src/app/core/services/general.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent extends BaseComponent implements OnInit {
  courses: Course[] = [];
  constructor(
    private readonly generalService: GeneralService) {
    super();
  }

  ngOnInit(): void {
    this.getCourses();
  }
  getCourses() {
    this.subscriptions.push(
      this.generalService.getCourses().subscribe((response: ResponseModel<Course[]>) => {
        this.courses = response.payload;
      })
    );
  }
}
