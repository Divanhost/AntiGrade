import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseComponent, BaseFormComponent } from 'src/app/shared/classes';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExamType } from 'src/app/shared/enums/exam-type.enum';
import { Employee } from 'src/app/shared/models/employee.model';

@Component({
  selector: 'app-add-edit-subject',
  templateUrl: './add-edit-subject.component.html',
  styleUrls: ['./add-edit-subject.component.scss']
})
export class AddEditSubjectComponent extends BaseFormComponent implements OnInit {
  isCreate: boolean;
  subjectId: number;
  teachers = [];
  examTypes: ExamType[] = [];
  dropdownList = [];
  selectedTeachers = [];
  dropdownSettings = {};
  constructor(private readonly router: Router,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
    this.isCreate = this.subjectId === undefined;
    console.log(this.isCreate);
  }

  ngOnInit(): void {
    this.dropdownList = [
      { item_id: 1, item_text: 'Mumbai' },
      { item_id: 2, item_text: 'Bangaluru' },
      { item_id: 3, item_text: 'Pune' },
      { item_id: 4, item_text: 'Navsari' },
      { item_id: 5, item_text: 'New Delhi' }
    ];
    this.selectedTeachers = [
      { item_id: 3, item_text: 'Pune' },
      { item_id: 4, item_text: 'Navsari' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }
  initForm() {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      examType: new FormControl('', [Validators.required, Validators.maxLength(500)]),
      teachersList: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      mainTeacher: new FormControl(null, Validators.required)
    });
  }
  onSubmit(){
    
  }

}
