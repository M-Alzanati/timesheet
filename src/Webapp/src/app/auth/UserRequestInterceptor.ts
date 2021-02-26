import { Injectable, Injector } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { AuthenticationService } from "./auth.service";
import { catchError } from "rxjs/operators";
import { Observable, of, throwError } from "rxjs";
import { Router } from "@angular/router";

@Injectable()
export class UserRequestInterceptor implements HttpInterceptor {
    constructor(private injector: Injector, private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        let authService = this.injector.get(AuthenticationService);
        const accessToken = authService?.getAccessToken();

        if (!accessToken) {
            return next.handle(req);
        }

        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });
        
        return next.handle(req).pipe(catchError(x => this.handleAuthError(x)));
    }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        if (err.status === 401 || err.status === 403) {
            this.router.navigateByUrl(`/login`);
            return of(err.message);
        }
        return throwError(err);
    }
}