import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { registerLocaleData, CommonModule } from '@angular/common';
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
import { ExamTableComponent } from './exam-table/exam-table.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {TabsModule} from 'ngx-tabset';
import { SubjectTabsetComponent } from './subject-tabset/subject-tabset.component';
import {MatSidenavModule} from '@angular/material/sidenav';
import { TeachersComponent } from './teachers/teachers.component';
import { SubjectCommonsComponent } from './subject-commons/subject-commons.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GroupComponent } from './group/group.component';
import { PartialTableComponent } from './partial-table/partial-table.component';
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
    DashboardComponent,
    ExamTableComponent,
    SubjectTabsetComponent,
    TeachersComponent,
    SubjectCommonsComponent,
    GroupComponent,
    PartialTableComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    MatCheckboxModule,
    MatSidenavModule,
    FontAwesomeModule,
    NgMultiSelectDropDownModule.forRoot(),
    MDBBootstrapModule.forRoot(),
    TabsModule.forRoot(),
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'always' }),
  ],
  providers: [
    LoginService,
    HttpService
  ],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class MainModule { }
