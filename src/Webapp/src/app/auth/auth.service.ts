import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from './models/login-model';
import { Observable, of } from 'rxjs';
import { BaseService } from '../base.service';
import { LogTimeRecord } from './models/log-time';

@Injectable()
export class AuthenticationService extends BaseService {
    authenticated: boolean = false;
    name: string;

    constructor(private router: Router, private http: HttpClient) {
        super();
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

    login(model: LoginModel): Observable<boolean> {
        return this.http.post('/api/account/login', model, this.httpOptions).pipe(
            map((response: any) => {
                if (response && response.token) {
                    this.authenticated = true;
                    localStorage.setItem('token', response.token);
                    localStorage.setItem('fullName', response.fullName);
                    localStorage.setItem('email', model.Email);
                    localStorage.setItem('uuid', response.id);

                    var now = new Date();
                    this.saveLogin(now.toUTCString()).subscribe();
                }
                return this.authenticated;
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
                    var now = new Date();
                    this.saveLogout(now.toUTCString()).subscribe();
                    localStorage.removeItem('token');
                    localStorage.removeItem('fullName');
                    localStorage.removeItem('email');
                    localStorage.removeItem('uuid');
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

    authenticate(): Observable<boolean> {
        return this.http.post('/api/account/authenticate', null, this.httpOptions).pipe(
            map((res) => {
                if (res) return true;
                return false;
            }),
            catchError(e => {
                return of(false);
            })
        );
    }

    getAccessToken(): string {
        return localStorage.getItem('token');
    }

    getFullName(): string {
        return localStorage.getItem('fullName');
    }

    getEmail(): string {
        return localStorage.getItem('email');
    }

    getUUId(): string {
        return localStorage.getItem('uuid');
    }

    saveLogin(time: string): Observable<boolean> {
        let model: LogTimeRecord = {
            Time: time,
            UUId: this.getUUId()
        };
        return this.saveLogTime('/api/timeSheet/logins/add', model);
    }

    saveLogout(time: string): Observable<boolean> {
        let model: LogTimeRecord = {
            Time: time,
            UUId: this.getUUId()
        };
        return this.saveLogTime('/api/timeSheet/logouts/add', model);
    }

    getLogins(): Observable<LoginModel[]> {
        return this.getLogTimes(`/api/timeSheet/logins/${this.getUUId()}`);
    }

    getLogouts(): Observable<LoginModel[]> {
        return this.getLogTimes(`/api/timeSheet/logouts/${this.getUUId()}`);
    }

    getLastLogin(): Observable<string> {
        return this.http.get(`/api/timeSheet/logins/last/${this.getUUId()}`, this.httpOptions)
            .pipe(
                map((res: any) => {
                    if (res) return res.time;
                    return null;
                }),
                catchError(e => {
                    return of(e);
                })
            );
    }

    getLastLogout(): Observable<string> {
        return this.http.get(`/api/timeSheet/logouts/last/${this.getUUId()}`, this.httpOptions)
            .pipe(
                map((res: any) => {
                    if (res) return res.time;
                    return null;
                }),
                catchError(e => {
                    return of(e);
                })
            );
    }

    private getLogTimes(url: string): Observable<LoginModel[]> {
        return this.http.get(url).pipe(
            map((response) => {
                if (response) {
                    return [];
                } else {
                    return [];
                }
            }),
            catchError(e => {
                return of([]);
            })
        );
    }

    private saveLogTime(url: string, model: LogTimeRecord): Observable<boolean> {
        return this.http.post(url, model, this.httpOptions).pipe(
            map((response) => {
                if (response) {
                    return true;
                } else {
                    return false;
                }
            }),
            catchError(e => {
                return of(false);
            })
        );
    }
}
