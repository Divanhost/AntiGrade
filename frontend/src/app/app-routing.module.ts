import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './modules/login/login.component';
import { UserEditCreateComponent } from './modules/user-edit-create/user-edit-create/user-edit-create.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UserEditCreateComponent },
  { path: 'dashboard', component: DashboardComponent },
  // { path: '404', component: PageNotFoundComponent },
  { path: '**', redirectTo: '404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
