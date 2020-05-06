import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Status } from 'src/app/shared/models/status.model';
import { Institute } from 'src/app/shared/models/institute.model';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  constructor(private http: HttpService) {

  }

  getCurrentMode(): Observable<ResponseModel<number>> {
    return this.http.getData(`general/mode`);
  }
  getAllStatuses(): Observable<ResponseModel<Status[]>> {
    return this.http.getData(`general/statuses`);
  }
  updateCurrentMode(id: number): Observable<ResponseModel<boolean>> {
    return this.http.getData(`general/mode/${id}`);
  }
  getInstitutes(): Observable<ResponseModel<Institute[]>> {
    return this.http.getData(`general/institutes`);
  }
}
