import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthenticationService } from 'src/app/auth/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private auth: AuthenticationService) {}

    canActivate() {
        return this.auth.authenticate().pipe(
            map((res) => {
                return true;
            }),
            catchError(e => {
                this.router.navigate(['/login']);
                return of(false);
            })
        );
    }
}
