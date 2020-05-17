import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { GeneralService } from 'src/app/core/services/general.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Mode } from 'src/app/shared/models/mode.model';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-mode',
  templateUrl: './change-mode.component.html',
  styleUrls: ['./change-mode.component.scss']
})
export class ChangeModeComponent extends BaseComponent implements OnInit {
  modes = [{id: 1 , name: 'Текущий учет'}, {id: 2, name: 'Экзамен'}, {id: 3, name: 'Пересдача'}];
  mode: Mode = new Mode();
  prevMode: Mode = new Mode();
  isChanged = false;
  constructor(
    private readonly generalService: GeneralService,
    private readonly notifierService: NotifierService,
    private router: Router
  ) {
    super();
   }

  ngOnInit(): void {
    this.getCurrentMode();
  }
  getCurrentMode() {
    this.subscriptions.push(
      this.generalService.getCurrentMode().subscribe((response: ResponseModel<number>) => {
        this.mode = this.modes.find(x => x.id === response.payload);
        this.prevMode = this.mode;
      })
    );
  }
  updateCurrentMode() {
    if (this.prevMode !== this.mode && this.isChanged) {
      this.subscriptions.push(
        this.generalService.updateCurrentMode(this.mode.id).subscribe((response: ResponseModel<boolean>) => {
          this.notifierService.notify('success', 'Режим успешно изменен');
          this.router.navigate(['/dashboard']);
        })
      );
    } else {
      this.notifierService.notify('error', 'Невозможно изменить режим');
    }
  }
  modeChanged() {
    this.isChanged = true;
  }
}
