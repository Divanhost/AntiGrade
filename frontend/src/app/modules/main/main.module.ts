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
import { AddEditSubjectComponent } from './add-edit-subject/add-edit-subject.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

registerLocaleData(localeRu, 'ru-RU');

@NgModule({
  declarations: [
    MainComponent,
    UserEditCreateComponent,
    AddEditSubjectComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgMultiSelectDropDownModule.forRoot(),
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'always' }),
  ],
  providers: [
    LoginService,
    HttpService
  ]
})
export class MainModule { }
