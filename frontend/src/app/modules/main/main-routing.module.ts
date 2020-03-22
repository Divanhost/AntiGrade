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
      { path: 'users',
        component: UserEditCreateComponent
      },
      { path: 'subjects',
        component: SubjectsComponent
      },
      { path: 'subjects/edit/:id',
        component: AddEditSubjectComponent
      },
      { path: 'subjects/add',
        component: AddEditSubjectComponent
      },
      { path: 'group/edit/:id',
        component: AddEditGroupComponent
      },
      { path: 'group/add',
        component: AddEditGroupComponent
      },
      { path: 'plan/edit/:id',
        component: AddEditPlanComponent
      },
      { path: 'plan/add/:id',
        component: AddEditPlanComponent
      },
      { path: 'subjects/rating/:id',
        component: RatingTableComponent
      },
      { path: 'subjects/groups/:id',
        component: GroupsComponent
      },
      { path: 'groups',
        component: GroupsComponent
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(appRoutes)],
  exports: [RouterModule]
})
export class MainRoutingModule {}
