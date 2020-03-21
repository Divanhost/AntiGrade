import { Component, OnInit, Input } from '@angular/core';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { StudentCriteria } from 'src/app/shared/models/student-criteria.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { BaseComponent } from 'src/app/shared/classes';
import { SubjectService } from 'src/app/core/services/subject.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';
import { StudentService } from 'src/app/core/services/student.service';
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
  editField: string;
  constructor(
    private readonly subjectService: SubjectService,
    private readonly studentService: StudentService,
    private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }

  ngOnInit(): void {
    this.initializeTable();
  }
  initializeTable() {
    this.getWorks();
    this.getStudents();
    this.getStudentCriteria();
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
  getStudentCriteria() {
    const studentIds = this.students.map(({id}) => id);
    this.subscriptions.push(
      this.studentService.getStudentCriterias(studentIds).subscribe((response: ResponseModel<StudentCriteria[]>) => {
        this.pointsDistribution = response.payload;
        console.log(this.pointsDistribution);
      })
    );
  }








  updateList(workId: number, studentId: number, event: any) {
    const editField = event.target.textContent;
  }

  changeValue(workId: number, studentId: number, event: any) {
    this.editField = event.target.textContent;
  }
}
