import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { NotifierService } from 'angular-notifier';
import { HttpService } from '.';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor(private readonly notifierService: NotifierService,
              private http: HttpService) { }
  public importFromFile(bstr: string): XLSX.AOA2SheetOpts {
    /* read workbook */
    const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

    /* grab first sheet */
    const wsname: string = wb.SheetNames[0];
    const ws: XLSX.WorkSheet = wb.Sheets[wsname];

    /* save data */
    const data =  (XLSX.utils.sheet_to_json(ws, { header: 1 })) as XLSX.AOA2SheetOpts;

    return data;
  }


  exportToFile(fileName: string, elementId: string) {
    return this.http.getData(`parse/1`);
    // if (!elementId) {
    //   this.notifierService.notify('error', 'Element Id does not exists');

    // }
    // const tbl = document.getElementById(elementId);
    // XLSX
    // const a = XLSX.utils.table_to_sheet(tbl);
    // const wb = XLSX.utils.table_to_book(tbl);
    // XLSX.writeFile(wb, fileName + '.xlsx');

  }
}
