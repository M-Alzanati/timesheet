import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from './models/login-model';
import { Observable, of } from 'rxjs';
import { BaseService } from '../base.service';

@Injectable()
export class AuthenticationService extends BaseService {
    authenticated: boolean = false;
    name: string;

    constructor(private router: Router, private http: HttpClient) {
        super();
    }

    login(model: LoginModel): Observable<boolean> {
        return this.http.post('/api/account/login', model, this.httpOptions).pipe(
            map((response: any) => {
                if (response) {
                    this.authenticated = true;
                    localStorage.setItem('token', response.token);
                }
                return this.authenticated;
            }),
            catchError(e => {
                this.authenticated = false;
                return of(false);
            }));
    }

    register(model: LoginModel): Observable<boolean> {
        return this.http.post('/api/account/register', model, this.httpOptions).pipe(
            map(response => {
                if (response) {
                    return true;
                } else {
                    return false;
                }
            }),
            catchError(e => {
                this.authenticated = false;
                return of(false);
            }));
    }

    logout(): Observable<boolean> {
        return this.http.post('/api/account/logout', null, this.httpOptions).pipe(
            map(response => {
                if (response) {
                    localStorage.removeItem('token');
                    return true;
                } else {
                    return false;
                }
            }),
            catchError(e => {
                this.authenticated = false;
                return of(false);
            }));
    }

    getAccessToken(): string {
        return localStorage.getItem('token');
    }
}
