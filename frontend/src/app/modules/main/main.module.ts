import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainRoutingModule } from './main-routing.module';
import localeRu from '@angular/common/locales/ru';
import { MainComponent } from './main.component';
import { UserEditCreateComponent } from './user-edit-create/user-edit-create.component';
import { LoginService, HttpService } from 'src/app/core/services';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { AddEditSubjectComponent } from './add-edit-subject/add-edit-subject.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AddEditGroupComponent } from './add-edit-group/add-edit-group.component';
import { AddEditPlanComponent } from './add-edit-plan/add-edit-plan.component';
import { RatingTableComponent } from './rating-table/rating-table.component';
import { SubjectsComponent } from './subjects/subjects.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { GroupsComponent } from './groups/groups.component';
import { AddEditEmployeeComponent } from './add-edit-employee/add-edit-employee.component';
import { DashboardComponent } from './dashboard/dashboard.component';
registerLocaleData(localeRu, 'ru-RU');

@NgModule({
  declarations: [
    MainComponent,
    UserEditCreateComponent,
    AddEditSubjectComponent,
    AddEditGroupComponent,
    AddEditPlanComponent,
    RatingTableComponent,
    SubjectsComponent,
    GroupsComponent,
    AddEditEmployeeComponent,
    DashboardComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgMultiSelectDropDownModule.forRoot(),
    MDBBootstrapModule.forRoot(),
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'always' }),
  ],
  providers: [
    LoginService,
    HttpService
  ],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class MainModule { }
