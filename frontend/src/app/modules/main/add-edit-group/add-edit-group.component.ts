import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from 'src/app/shared/classes';
import { Group } from 'src/app/shared/models/group.model';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { Student } from 'src/app/shared/models/student.model';
@Component({
  selector: 'app-add-edit-group',
  templateUrl: './add-edit-group.component.html',
  styleUrls: ['./add-edit-group.component.scss']
})
export class AddEditGroupComponent extends BaseFormComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  isCreate: boolean;
  groupId: number;
  group: Group = new Group();
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly groupService: GroupService) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.groupId = params.id)
    );
    this.isCreate = this.groupId === undefined;
  }
  ngOnInit(): void {
    this.initForm();
  }
  initForm() {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    });
    this.group.students = [];
    this.addStudent();
  }
  addStudent() {
    this.group.students.push(new Student());
  }
  onSubmit() {
    this.subscriptions.push(
      this.groupService.addGroup(this.group).subscribe()
    );
  }
}
