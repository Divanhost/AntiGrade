import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Employee } from 'src/app/shared/models/employee.model';
import { Group } from 'src/app/shared/models/group.model';
import { ExamType } from 'src/app/shared/models/exam-type.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from 'src/app/core/services/employee.service';
import { SubjectService } from 'src/app/core/services/subject.service';
import { GroupService } from 'src/app/core/services/group.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectEmployee } from 'src/app/shared/models/subject-employee.model';

@Component({
  selector: 'app-subject-commons',
  templateUrl: './subject-commons.component.html',
  styleUrls: ['./subject-commons.component.scss']
})
export class SubjectCommonsComponent extends BaseFormComponent implements OnInit {

  isCreate: boolean;
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  // @Output() changeData: EventEmitter<SubjectDto> = new EventEmitter();
  examTypes: ExamType[] = [];
  name: string;
  examType: ExamType = new ExamType();
  constructor(private readonly employeeService: EmployeeService,
              private readonly subjectService: SubjectService,
              private readonly groupService: GroupService) {
    super();
  }

  ngOnInit(): void {
    this.name = this.subject.name;
    //this.examType = this.subject.examType;
    this.getExamTypes();
  }

  // getSubject() {
  //   this.subscriptions.push(
  //     this.subjectService.getSubject(this.subjectId).subscribe((response: ResponseModel<SubjectDto>) => {
  //       this.subject = response.payload;
  //       this.subject.examType = this.examTypes
  //     })
  //   );
  // }
  // fillForm() {
  //   this.form.controls.name.setValue(this.subject.name);
  //   this.form.controls.examType.setValue(this.subject.examType);
  //   this.form.controls.teachers.setValue(this.selectedEmployees);
  //   this.form.controls.mainTeacher.setValue(this.mainTeacher);
  // }
  submit() {
    // this.convertEmployees();
    // if (this.isCreate) {
    //   this.subscriptions.push(
    //     this.subjectService.addSubject(this.subject).subscribe(() => {
    //       this.router.navigate(['/subjects']);
    //     })
    //   );
    // } else {
    //   this.subscriptions.push(
    //     this.subjectService.updateSubject(this.subjectId, this.subject).subscribe(() => {
    //       this.router.navigate(['/subjects']);
    //     })
    //   );
    // }
    // this.changeData.emit(this.subject);
  }
  getExamTypes() {
    this.subscriptions.push(
      this.subjectService.getExamTypes().subscribe((responce: ResponseModel<ExamType[]>) => {
        this.examTypes = responce.payload;
        if (this.subject) {
          this.examType = this.examTypes.find(x => x.id === this.subject.examType.id);
        }
      })
    );
  }
  // getGroups() {
  //   this.subscriptions.push(
  //     this.groupService.getGroups().subscribe((responce: ResponseModel<Group[]>) => {
  //       this.groups = responce.payload;
  //     })
  //   );
  // }
}
