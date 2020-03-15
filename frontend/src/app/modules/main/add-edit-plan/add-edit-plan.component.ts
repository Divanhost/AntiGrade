import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Work } from 'src/app/shared/models/work.model';
import { Student } from 'src/app/shared/models/student.model';
import { Criteria } from 'src/app/shared/models/criteria.model';

@Component({
  selector: 'app-add-edit-plan',
  templateUrl: './add-edit-plan.component.html',
  styleUrls: ['./add-edit-plan.component.scss']
})
export class AddEditPlanComponent extends BaseFormComponent implements OnInit {

  isCreate: boolean;
  planId: number;
  works: Work[] = [];
  students: Student[] = [];
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly groupService: GroupService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.planId = params.id)
    );
    this.isCreate = this.planId === undefined;
  }

  ngOnInit(): void {
    this.students.push({name: 'Angelina'});
    this.students.push({name: 'George'});
    this.students.push({name: 'Ivan'});
  }
  addWork() {
    const work = new Work();
    work.criterias = [];
    this.works.push(work);
  }
  addCriteria(work: Work) {
    work.criterias.push(new Criteria());
  }
  savePlan() {
    console.log(this.works);
  }
}
