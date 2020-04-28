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
  examTypes: ExamType[] = [];
  name: string;
  examType: ExamType = new ExamType();
  constructor(private readonly employeeService: EmployeeService,
              private readonly subjectService: SubjectService,
              private readonly groupService: GroupService) {
      super();
  }

  ngOnInit(): void {
    this.updateData(this.subject);
  }
  getExamTypes() {
    this.subscriptions.push(
      this.subjectService.getExamTypes().subscribe((responce: ResponseModel<ExamType[]>) => {
        this.examTypes = responce.payload;
        if (this.subject.examType) {
          this.examType = this.examTypes.find(x => x.id === this.subject.examType.id);
        }
      })
    );
  }
  updateData(subject: SubjectDto) {
    this.subject = subject;
    this.name = subject.name;
    this.getExamTypes();
  }
}
