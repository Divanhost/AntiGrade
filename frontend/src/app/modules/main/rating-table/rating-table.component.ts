import { Component, OnInit, Input } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-rating-table',
  templateUrl: './rating-table.component.html',
  styleUrls: ['./rating-table.component.scss']
})
export class RatingTableComponent extends BaseComponent implements OnInit {

  subject: SubjectDto;
  works: Work[];
  students: Student[];
  subjectId: number;
  pointsDistribution: StudentCriteria[];
  constructor(
    private readonly subjectService: SubjectService,
    private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.initializeTable();
  }
  editField: string;
  personList: Array<any> = [
    { id: 1, name: 'Aurelia Vega', age: 30, companyName: 'Deepends', country: 'Spain', city: 'Madrid' },
    { id: 2, name: 'Guerra Cortez', age: 45, companyName: 'Insectus', country: 'USA', city: 'San Francisco' },
    { id: 3, name: 'Guadalupe House', age: 26, companyName: 'Isotronic', country: 'Germany', city: 'Frankfurt am Main' },
    { id: 4, name: 'Aurelia Vega', age: 30, companyName: 'Deepends', country: 'Spain', city: 'Madrid' },
    { id: 5, name: 'Elisa Gallagher', age: 31, companyName: 'Portica', country: 'United Kingdom', city: 'London' },
  ];

  awaitingPersonList: Array<any> = [
    { id: 6, name: 'George Vega', age: 28, companyName: 'Classical', country: 'Russia', city: 'Moscow' },
    { id: 7, name: 'Mike Low', age: 22, companyName: 'Lou', country: 'USA', city: 'Los Angeles' },
    { id: 8, name: 'John Derp', age: 36, companyName: 'Derping', country: 'USA', city: 'Chicago' },
    { id: 9, name: 'Anastasia John', age: 21, companyName: 'Ajo', country: 'Brazil', city: 'Rio' },
    { id: 10, name: 'John Maklowicz', age: 36, companyName: 'Mako', country: 'Poland', city: 'Bialystok' },
  ];

  initializeTable() {
    this.getWorks();
    this.getStudents();

  }

  getWorks() {
    this.subscriptions.push(
      this.subjectService.getSubjectWorks(this.subjectId).subscribe((response: ResponseModel<Work[]>) => {
        this.works = response.payload;
        console.log(this.works);
      })
    );
  }
  getStudents() {
    this.subscriptions.push(
      this.subjectService.getSubjectStudents(this.subjectId).subscribe((response: ResponseModel<Student[]>) => {
        this.students = response.payload;
        console.log(this.students);
      })
    );
  }








  updateList(id: number, property: string, event: any) {
    const editField = event.target.textContent;
    this.personList[id][property] = editField;
  }

  remove(id: any) {
    this.awaitingPersonList.push(this.personList[id]);
    this.personList.splice(id, 1);
  }

  add() {
    if (this.awaitingPersonList.length > 0) {
      const person = this.awaitingPersonList[0];
      this.personList.push(person);
      this.awaitingPersonList.splice(0, 1);
    }
  }

  changeValue(id: number, property: string, event: any) {
    this.editField = event.target.textContent;
  }
}
