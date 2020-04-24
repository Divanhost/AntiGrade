import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/shared/models/response.model';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  constructor(private http: HttpService) {

  }

  getCurrentMode(): Observable<ResponseModel<number>> {
    return this.http.getData(`general/mode`);
  }
  updateCurrentMode(id: number): Observable<ResponseModel<boolean>> {
    // const params = HttpService.toHttpParams({number: id});
    return this.http.getData(`general/mode/${id}`);
  }
}
