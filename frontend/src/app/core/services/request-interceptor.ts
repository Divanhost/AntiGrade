import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators';
import { NotifierService } from 'angular-notifier';
import { LoginService } from './login.service';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  private readonly notifier: NotifierService;

  constructor(notifierService: NotifierService,
              private loginService: LoginService) {
    this.notifier = notifierService;
  }
  /**
   * intercept all XHR request
   * @param request//
   * @param next//
   * @returns Observable<A>
   */
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let jwt = '';
    if (localStorage.getItem('jwtToken')) {
      jwt = localStorage.getItem('jwtToken').replace(/"/g, '');
      request = this.addToken(request, jwt);
    }

    return next.handle(request).pipe(catchError((error) => {
      if (error.status === 401) {
        if (localStorage.getItem('jwtToken')) {
          this.notifier.notify('error', 'Сеанс истек');
          this.loginService.logout();
        }
      } else {
        this.notifier.notify('error', error.error);
      }
      return of(error);
    }) as any);
  }

  private addToken(request: HttpRequest<any>, token: string) {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        'Access-Control-Allow-Origin': '*'
      }
    });

    request.headers.set('Access-Control-Allow-Origin', '*');

    return request;
  }
}
