import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainRoutingModule } from './main-routing.module';
import localeRu from '@angular/common/locales/ru';
import { MainComponent } from './main.component';
import { UserEditCreateComponent } from './user-edit-create/user-edit-create.component';
import { LoginService, HttpService } from 'src/app/core/services';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';


registerLocaleData(localeRu, 'ru-RU');

@NgModule({
  declarations: [
    MainComponent,
    UserEditCreateComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'always' }),
  ],
  providers: [
    LoginService,
    HttpService
  ]
})
export class MainModule { }
