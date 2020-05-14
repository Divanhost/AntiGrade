import { Component, OnInit } from '@angular/core';
import { Institute } from 'src/app/shared/models/institute.model';
import { GeneralService } from 'src/app/core/services/general.service';
import { BaseComponent } from 'src/app/shared/classes';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Component({
  selector: 'app-institutes',
  templateUrl: './institutes.component.html',
  styleUrls: ['./institutes.component.scss']
})
export class InstitutesComponent extends BaseComponent implements OnInit {
  institutes: Institute[] = [];
  constructor(
    private readonly generalService: GeneralService) {
    super();
  }

  ngOnInit(): void {
    this.getInstitutes();
  }
  getInstitutes() {
    this.subscriptions.push(
      this.generalService.getInstitutes().subscribe((response: ResponseModel<Institute[]>) => {
        this.institutes = response.payload;
      })
    );
  }
}
