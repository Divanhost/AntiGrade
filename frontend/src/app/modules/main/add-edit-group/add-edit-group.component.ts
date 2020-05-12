import { Component, OnInit } from '@angular/core';
import { BaseFormComponent, BaseComponent } from 'src/app/shared/classes';
import { Group } from 'src/app/shared/models/group.model';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { Student } from 'src/app/shared/models/student.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { NotifierService } from 'angular-notifier';
import { Course } from 'src/app/shared/models/course.model';
@Component({
  selector: 'app-add-edit-group',
  templateUrl: './add-edit-group.component.html',
  styleUrls: ['./add-edit-group.component.scss']
})
export class AddEditGroupComponent extends BaseComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  isCreate: boolean;
  courses: Course[] = [];
  groupId: number;
  group: Group = new Group();
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notifierService: NotifierService,
              private readonly groupService: GroupService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.groupId = params.id)
    );
    this.isCreate = this.groupId === undefined;
  }
  ngOnInit(): void {
    this.initGroup();
  }
  initGroup() {
    if (this.isCreate) {
      this.group.students = [];
      this.addStudent();
      this.getCourses();
    } else {
      this.getCourses();
    }
  }
  getCourses() {
      this.subscriptions.push(
        this.groupService.getCourses().subscribe((response: ResponseModel<Course[]>) => {
          this.courses = response.payload;
          if (!this.isCreate) {
            this.getGroup();
          }
        })
      );
  }
  getGroup() {
    this.subscriptions.push(
      this.groupService.getGroup(this.groupId).subscribe((response: ResponseModel<Group>) => {
        this.group = response.payload;
        this.group.course = this.courses.find(x => x.id === this.group.course.id);
      })
    );
}
  addStudent() {
    const student = new Student();
    student.groupId = this.groupId;
    this.group.students.push(student);
  }
  removeStudent(student: Student) {
    this.group.students = this.group.students.filter(x => x !== student);
  }
  onSubmit() {
    this.group.students = this.group.students.filter(x => x.lastName !== '' && x.firstName !== '');
    if (this.group.name === null || this.group.name === '' || this.group.name === undefined) {
      this.notifierService.notify('error', 'Нельзя создать группу');
      return;
    }
    if (this.isCreate) {
      this.subscriptions.push(
        this.groupService.addGroup(this.group).subscribe(() => {
          this.router.navigate(['/groups']);
        })
      );
    } else {
      this.subscriptions.push(
        this.groupService.updateGroup(this.groupId, this.group).subscribe(() => {
          this.router.navigate(['/groups']);
        })
      );
    }
  }
}
