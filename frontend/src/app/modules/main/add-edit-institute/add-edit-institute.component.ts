import { Component, OnInit } from '@angular/core';
import { Institute } from 'src/app/shared/models/institute.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GeneralService } from 'src/app/core/services/general.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Department } from 'src/app/shared/models/department.model';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-add-edit-institute',
  templateUrl: './add-edit-institute.component.html',
  styleUrls: ['./add-edit-institute.component.scss']
})
export class AddEditInstituteComponent extends BaseComponent implements OnInit {
  instituteId: number;
  institute: Institute = new Institute();
  isCreate: boolean;
  constructor(private readonly generalService: GeneralService,
              private readonly notifierService: NotifierService,
              private readonly router: Router,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.instituteId = params.id)
    );
    this.isCreate = this.instituteId === undefined;
   }

  ngOnInit(): void {
    if (!this.isCreate) {
      this.getInstitute();
    } else {
      this.institute.departments = [];
      this.addDepartment();
    }
  }
  getInstitute() {
    this.subscriptions.push(
      this.generalService.getInstitute(this.instituteId).subscribe((response: ResponseModel<Institute>) => {
        this.institute = response.payload;
      })
    );
  }
  addDepartment() {
    const department = new Department();
    department.name = null;
    this.institute.departments.push(department);
  }
  removeDepartment(department: Department) {
    this.institute.departments = this.institute.departments.filter(x => x !== department);
  }
  onSubmit() {
    this.institute.departments = this.institute.departments.filter(x => x.name !== '' && x.name !== null);
    if (this.institute.name === null || this.institute.name === '' || this.institute.name === undefined) {
      this.notifierService.notify('error', 'Нельзя создать группу');
      return;
    }
    if (this.isCreate) {
      this.subscriptions.push(
        this.generalService.createInstitute(this.institute).subscribe(() => {
          this.router.navigate(['/institutes']);
        })
      );
    } else {
      this.subscriptions.push(
        this.generalService.updateInstitute(this.institute).subscribe(() => {
          this.router.navigate(['/institutes']);
        })
      );
    }
  }
}
