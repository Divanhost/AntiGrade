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
  loaded = 0;
  isLoaderShown = true;
  constructor(private readonly subjectService: SubjectService) {
    super();
   }

  ngOnInit(): void {
    this.getSubjects(this.loaded);
  }
  getSubjects(skip: number) {
    this.subscriptions.push(
      this.subjectService.getSubjects(skip).subscribe((responce: ResponseModel<SubjectView[]>) => {
        this.subjects = Array.prototype.concat(this.subjects, responce.payload);
        if (!responce.payload.length) {
          this.isLoaderShown = false;
        }
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
  loadMore() {
    this.loaded += 8;
    this.getSubjects(this.loaded);
  }
}
