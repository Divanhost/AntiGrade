import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { AdminGuard } from 'src/app/core/guards/admin.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserEditCreateComponent } from './user-edit-create/user-edit-create.component';
import { AddEditSubjectComponent } from './add-edit-subject/add-edit-subject.component';
import { AddEditGroupComponent } from './add-edit-group/add-edit-group.component';

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
      { path: 'subject/edit/:id',
        component: AddEditSubjectComponent
      },
      { path: 'subject/add',
        component: AddEditSubjectComponent
      },
      { path: 'group/edit/:id',
        component: AddEditGroupComponent
      },
      { path: 'group/add',
        component: AddEditGroupComponent
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(appRoutes)],
  exports: [RouterModule]
})
export class MainRoutingModule {}
