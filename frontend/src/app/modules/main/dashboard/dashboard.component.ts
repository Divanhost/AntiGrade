import { Component, OnInit } from '@angular/core';
import { SubjectService } from 'src/app/core/services/subject.service';
import { SubjectView } from 'src/app/shared/models/subject-view.model';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { GroupService } from 'src/app/core/services/group.service';
import { Group } from 'src/app/shared/models/group.model';
import { MainSubjectView } from 'src/app/shared/models/main-subject-view.model';
import { Mode } from 'src/app/shared/models/mode.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GeneralService } from 'src/app/core/services/general.service';
import { Semester } from 'src/app/shared/models/semester.model';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {
  mode: Mode = new Mode();
  modes = [{id: 1 , name: 'Текущий учет'}, {id: 2, name: 'Экзамен'}, {id: 3, name: 'Пересдача'}];
  subjects: MainSubjectView[];
  subjectGroups = [];
  semesters: Semester[] = [];
  currentSemester: Semester = new Semester();
  constructor(
    private readonly subjectService: SubjectService,
    private readonly groupService: GroupService,
    private readonly generalService: GeneralService
  ) {
    super();
   }

  ngOnInit(): void {
    this.getSemesters();
  }
  getSubjects() {
    this.subjectService.getDistinctSubjects(this.currentSemester.id).subscribe((response: ResponseModel<MainSubjectView[]>) => {
      this.subjects = response.payload;
      console.log(this.subjects);
      this.getSubjectGroups();
    });
  }
  getSubjectGroups() {
    this.subjects.forEach(element => {
      this.subjectService.getSubjectsByName(element.name, this.currentSemester.id).subscribe((response: ResponseModel<SubjectView[]>) => {
        this.subjectGroups.push({subjectName: element.name, subjects: response.payload});
        this.subjectGroups.sort((a, b) => a.subjectName.localeCompare(b.subjectName));
      });
    });
  }
  getCurrentMode() {
    this.subscriptions.push(
      this.generalService.getCurrentMode().subscribe((response: ResponseModel<number>) => {
        this.mode = this.modes.find(x => x.id === response.payload);
        this.getSubjects();
      })
    );
  }
  getSemesters() {
    this.subscriptions.push(
      this.generalService.getSemesters().subscribe((response: ResponseModel<Semester[]>) => {
        this.semesters = response.payload;
        this.currentSemester = this.semesters[0];
        this.getCurrentMode();
      })
    );
  }
  reloadSubjects() {
    this.subjects = [];
    this.subjectGroups = [];
    this.getSubjects();
  }
}
