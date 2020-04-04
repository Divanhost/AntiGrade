import { Component, OnInit } from '@angular/core';
import { SubjectService } from 'src/app/core/services/subject.service';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { GroupService } from 'src/app/core/services/group.service';
import { Group } from 'src/app/shared/models/group.model';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  mode = 2;
  subjects: SubjectView[];
  subjectGroups = [];
  constructor(
    private readonly subjectService: SubjectService,
    private readonly groupService: GroupService
  ) { }

  ngOnInit(): void {
    this.getSubjects();
  }
  getSubjects() {
    this.subjectService.getDistinctSubjects().subscribe((response: ResponseModel<SubjectView[]>) => {
      this.subjects = response.payload;
      console.log(this.subjects);
      this.getSubjectGroups();
    });
  }
  getSubjectGroups() {
    this.subjects.forEach(element => {
      this.groupService.getSubjectGroupsByName(element.name).subscribe((response: ResponseModel<Group[]>) => {
        this.subjectGroups.push({subject: element, groups: response.payload});
        console.log(this.subjectGroups);
      });
    });
  }
}
