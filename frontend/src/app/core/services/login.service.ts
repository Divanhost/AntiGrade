import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpService } from './http.service';
import { tap } from 'rxjs/internal/operators';
import { Observable } from 'rxjs';
import { LoginModel, TokenCouple } from 'src/app/shared/models';

@Injectable({ providedIn: 'root' })
export class LoginService {
    private readonly JWT_TOKEN = 'jwtToken';
    private readonly REFRESH_TOKEN = 'refreshToken';

    constructor(private httpService: HttpService,
                private router: Router) {
    }

    login(loginModel: LoginModel): Observable<string> {
        return this.httpService.postData(`auth/login`, loginModel);
    }

    renew() {
        const jwt = localStorage.getItem(this.JWT_TOKEN);
        const refresh = localStorage.getItem(this.REFRESH_TOKEN);
        const oldTokens = new TokenCouple(jwt, refresh);
        return this.httpService.postData('auth/renew', oldTokens ).pipe(tap((tokens: TokenCouple) => {
            this.storeToken(tokens.jwt);
        }));
    }

    storeToken(token: string) {
        localStorage.setItem(this.JWT_TOKEN, token);
    }

    tokenExistAndNotExpired(): boolean {
        return localStorage.getItem(this.JWT_TOKEN) !== null;
    }

    logout() {
        localStorage.clear();
        localStorage.setItem('lastUrl', this.router.url);
        this.router.navigate(['/login']);
    }
}
