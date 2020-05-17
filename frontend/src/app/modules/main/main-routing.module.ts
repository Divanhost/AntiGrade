import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { AdminGuard } from 'src/app/core/guards/admin.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserEditCreateComponent } from './user-edit-create/user-edit-create.component';
import { AddEditSubjectComponent } from './add-edit-subject/add-edit-subject.component';
import { AddEditGroupComponent } from './add-edit-group/add-edit-group.component';
import { AddEditPlanComponent } from './add-edit-plan/add-edit-plan.component';
import { SubjectsComponent } from './subjects/subjects.component';
import { RatingTableComponent } from './rating-table/rating-table.component';
import { GroupsComponent } from './groups/groups.component';
import { AddEditEmployeeComponent } from './add-edit-employee/add-edit-employee.component';
import { TeachersComponent } from './teachers/teachers.component';
import { ExamTableComponent } from './exam-table/exam-table.component';
import { UsersComponent } from './users/users.component';
import { EmployeesComponent } from './employees/employees.component';
import { InstitutesComponent } from './institutes/institutes.component';
import { AddEditInstituteComponent } from './add-edit-institute/add-edit-institute.component';
import { CoursesComponent } from './courses/courses.component';
import { AddEditCourseComponent } from './add-edit-course/add-edit-course.component';
import { ChangeModeComponent } from './change-mode/change-mode.component';
import { SemesterComponent } from './semester/semester.component';

const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  {
    path: '',
    component: MainComponent,
    canActivate: [AdminGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'users',
        component: UsersComponent
      },
      {
        path: 'users/add',
        component: UserEditCreateComponent
      },
      {
        path: 'users/edit/:id',
        component: UserEditCreateComponent
      },
      {
        path: 'subjects',
        component: SubjectsComponent
      },
      {
        path: 'subjects/edit/:id',
        component: AddEditSubjectComponent
      },
      {
        path: 'subjects/add',
        component: AddEditSubjectComponent
      },
      {
        path: 'groups/edit/:id',
        component: AddEditGroupComponent
      },
      {
        path: 'groups/add',
        component: AddEditGroupComponent
      },
      {
        path: 'plan/edit/:id',
        component: AddEditPlanComponent
      },
      {
        path: 'plan/add/:id',
        component: AddEditPlanComponent
      },
      {
        path: 'subjects/rating/:id',
        component: RatingTableComponent
      },
      {
        path: 'subjects/groups/:id',
        component: GroupsComponent
      },
      {
        path: 'groups',
        component: GroupsComponent
      },
      {
        path: 'employees',
        component: EmployeesComponent
      },
      {
        path: 'employees/edit/:id',
        component: AddEditEmployeeComponent
      },
      {
        path: 'employees/add',
        component: AddEditEmployeeComponent
      },
      {
        path: 'subjects/:id/teachers',
        component: TeachersComponent
      },
      {
        path: 'subjects/exam/:id',
        component: ExamTableComponent
      },
      {
        path: 'institutes',
        component: InstitutesComponent
      },
      {
        path: 'institutes/edit/:id',
        component: AddEditInstituteComponent
      },
      {
        path: 'institutes/add',
        component: AddEditInstituteComponent
      },
      {
        path: 'courses',
        component: CoursesComponent
      },
      {
        path: 'courses/edit/:id',
        component: AddEditCourseComponent
      },
      {
        path: 'courses/add',
        component: AddEditCourseComponent
      },
      {
        path: 'modes',
        component: ChangeModeComponent
      },
      {
        path: 'semester',
        component: SemesterComponent
      },
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(appRoutes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
