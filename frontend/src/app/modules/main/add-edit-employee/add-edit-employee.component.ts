import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { Employee } from 'src/app/shared/models/employee.model';

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.scss']
})
export class AddEditEmployeeComponent extends BaseComponent implements OnInit {

  constructor() { 
    super();
  }

  ngOnInit(): void {
  }

}
