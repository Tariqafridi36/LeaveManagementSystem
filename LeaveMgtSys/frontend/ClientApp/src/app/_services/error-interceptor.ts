import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './auth.service';
import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { throwError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private service: AuthService, private _cookieService: CookieService) { }
  intercept(
    req: import("@angular/common/http").HttpRequest<any>,
    next: import("@angular/common/http").HttpHandler
  ): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {

    return next.handle(req).pipe(

      catchError((error) => {
        debugger
        if (error.status === 0 || error.status === 401 || error.status === 404 || error.status === 403 || error.status === 400) {
         
          localStorage.removeItem('userName');
          localStorage.removeItem('token');
          
          return throwError(error.statusText);
        } 

        if (error.status === 404 || error.status === 500 || error.status === 504) {
          return throwError(error.statusText);
        }

        if (error.status === 3) {
          return throwError('Invalid request.');
        }

        if (error instanceof HttpErrorResponse) {

          const applicationError = error.headers.get("Application-Error");
          if (applicationError) {
            return throwError(applicationError);
          }
          let serverError = error.error;
          let modelStatusErrors = "";
          if (serverError.errors && typeof serverError.errors === "object") {
            for (const key in serverError.errors) {
              if (serverError.errors[key]) {
                modelStatusErrors += serverError.errors[key] + "\n";
              }
            }
          } else if (error.error.text !== '') {
            serverError = error.error.text;
          }
          return throwError(modelStatusErrors || serverError || "Server Error" || error.statusText);
          //return throwError(error.statusText);
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
