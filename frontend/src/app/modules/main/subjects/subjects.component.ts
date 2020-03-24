import { Component, OnInit } from '@angular/core';
import { SubjectService } from 'src/app/core/services/subject.service';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.scss']
})
export class SubjectsComponent extends BaseComponent implements OnInit {
  subjects: SubjectView[] = [];
  constructor(private readonly subjectService: SubjectService) {
    super();
   }

  ngOnInit(): void {
    this.getSubjects();
  }
  getSubjects() {
    this.subscriptions.push(
      this.subjectService.getSubjects().subscribe((responce: ResponseModel<SubjectView[]>) => {
        this.subjects = responce.payload;
      })
    );
  }
  removeSubject(subject: SubjectView) {
    this.subscriptions.push(
      this.subjectService.removeSubject(subject.id).subscribe(() => {
        this.subjects = this.subjects.filter(x => x !== subject);
      })
    );
  }
}
