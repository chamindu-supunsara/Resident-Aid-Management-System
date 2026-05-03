import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
  
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  
  constructor( private cookieService: CookieService, private _router: Router ) { }

  intercept( req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

      req = req.clone({
        setHeaders: {
          // "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Headers": "Content-Type",
          "Access-Control-Allow-Methods": "GET",
          "Access-Control-Allow-Origin": "*",
          Authorization: `Bearer ` + this.cookieService.get('e_jwt'),
          
        },
      });

      return next.handle(req).pipe(tap(() => { },
        (err: any) => {
          if (err instanceof HttpErrorResponse) {

            if (this._router.url === '/') {
              return;
            } else {
              if (err.status == 400) {
                // this.toastr.error('', 'Bad Request! Please retry!.', {
                //   timeOut: 6000, positionClass: 'toast-top-center'
                // });
              } else if (err.status == 401) {
                // this.toastr.error('', 'Request Unauthorized!, Redirecting...', {
                //   timeOut: 6000, positionClass: 'toast-top-center'
                // });
                this._router.navigate(['/']);
              } else {
                // this.toastr.error('', 'An unexpected error occurred!', {
                //   timeOut: 6000, positionClass: 'toast-top-center'
                // });
              }
            }
          }
        }
      ));
    }
  }
