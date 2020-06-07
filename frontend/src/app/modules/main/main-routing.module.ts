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
import { AdminRoleGuard } from 'src/app/core/guards/admin-role.guard';

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
        component: UsersComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'users/add',
        component: UserEditCreateComponent,
        canActivate: [AdminRoleGuard]
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
        path: 'subjects/rating/additional/:id',
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
        component: EmployeesComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'employees/edit/:id',
        component: AddEditEmployeeComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'employees/add',
        component: AddEditEmployeeComponent,
        canActivate: [AdminRoleGuard]
      },
      // {
      //   path: 'subjects/:id/teachers',
      //   component: TeachersComponent
      // },
      {
        path: 'subjects/exam/:id',
        component: ExamTableComponent
      },
      {
        path: 'institutes',
        component: InstitutesComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'institutes/edit/:id',
        component: AddEditInstituteComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'institutes/add',
        component: AddEditInstituteComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'courses',
        component: CoursesComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'courses/edit/:id',
        component: AddEditCourseComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'courses/add',
        component: AddEditCourseComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'modes',
        component: ChangeModeComponent,
        canActivate: [AdminRoleGuard]
      },
      {
        path: 'semester',
        component: SemesterComponent,
        canActivate: [AdminRoleGuard]
      },
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(appRoutes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
