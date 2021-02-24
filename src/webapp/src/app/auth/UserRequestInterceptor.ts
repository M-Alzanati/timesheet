import { Injectable, Injector } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";
import { AuthenticationService } from "./auth.service";

@Injectable()
export class UserRequestInterceptor implements HttpInterceptor {
    constructor(private injector: Injector) {
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
        
        return next.handle(req);
    }
}