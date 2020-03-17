import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import * as moment from 'moment';

const defaultHttpOptions = {
    headers: new HttpHeaders(
        {
            'Access-Control-Allow-Origin': '*',
            'Content-type': 'application/json'
        })
};

@Injectable()
export class HttpService {

    public host: string;

    constructor(private http: HttpClient) {
        this.host = environment.apiHost + 'api/';
    }

    get defaultOptions() {
        return defaultHttpOptions;
    }
    static toHttpParams(obj: any): HttpParams {
        return this.addToHttpParams(new HttpParams(), obj, null);
    }

    private static addToHttpParams(params: HttpParams, obj: any, prefix: string): HttpParams {
        for (const p in obj) {
            if (obj.hasOwnProperty(p)) {
                let k = p;
                if (prefix) {
                    if (p.match(/^-{0,1}\d+$/)) {
                        k = prefix + '[' + p + ']';
                    } else {
                        k = prefix + '.' + p;
                    }
                }
                const v = obj[p];
                if (v !== null && typeof v === 'object' && !(v instanceof Date)) {
                    params = this.addToHttpParams(params, v, k);
                } else if (v !== undefined) {
                    if (v instanceof Date) {
                        params = params.set(k, moment(v).format('LLLL'));
                    } else {
                        params = params.set(k, v);
                    }

                }
            }
        }
        return params;
    }

    static toHttpParamsWithoutNulls(obj: any): HttpParams {
        return this.addToHttpParamsWithoutNulls(new HttpParams(), obj, null);
    }

    private static addToHttpParamsWithoutNulls(params: HttpParams, obj: any, prefix: string): HttpParams {
        for (const p in obj) {
            if (obj.hasOwnProperty(p)) {
                let k = p;
                if (prefix) {
                    if (p.match(/^-{0,1}\d+$/)) {
                        k = prefix + '[' + p + ']';
                    } else {
                        k = prefix + '.' + p;
                    }
                }
                const v = obj[p];
                if (v !== null && typeof v === 'object' && !(v instanceof Date)) {
                    params = this.addToHttpParamsWithoutNulls(params, v, k);
                } else if (v !== undefined && v !== null && v !== '') {
                    if (v instanceof Date) {
                        params = params.set(k, moment(v).format('LLLL')); // serialize date as you want
                    } else {
                        params = params.set(k, v);
                    }

                }
            }
        }
        return params;
    }

    getParams(key: string, arrayParams: any[]) {
        let paramsStr = `${key}=`;
        arrayParams.forEach((i, idx, array) => {
            let keyParam = `&${key}=`;
            if (idx === array.length - 1) {
                keyParam = '';
            }

            paramsStr += `${i}${keyParam}`;
        });

        return paramsStr;
    }

    getData(route: string, httpOptions: any = defaultHttpOptions): Observable<any> {
        return this.http.get<any>(this.host + route, httpOptions);
    }

    postData(route: string, data: object): Observable<any> {
        return this.http.post(this.host + route, data);
    }
    postMData(route: string, data: object): Observable<any> {
        return  this.http.post<any>(this.host + route, data);
    }
    postJsonData(route: string, data: object): Observable<any> {
        const header = new HttpHeaders()
        .set('Content-type', 'application/json');
        return  this.http.post<any>(this.host + route, data, { headers: header});
    }

    putData(route: string, data: object): Observable<any> {
        return this.http.put(this.host + route, data);
    }

    deleteData(route: string): Observable<any> {
        return this.http.delete(this.host + route);
    }
}
