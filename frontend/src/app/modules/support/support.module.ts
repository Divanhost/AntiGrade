import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { CommonModule } from '@angular/common';
import { SupportRoutingModule } from './support-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { SupportComponent } from './support.component';
import { HttpService } from 'src/app/core/services';

@NgModule({
  declarations: [
    SupportComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,
    SupportRoutingModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'always' })
  ],
  providers: [
    HttpService
  ]
})
export class SupportModule { }
