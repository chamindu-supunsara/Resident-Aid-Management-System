import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthOTPRequest, AuthRequest, ResetPassword, UserForm } from '../Datamodels/daraforms';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Observable, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  _baseUrl!: string;
  apiURL = environment.rooturl + "/";
  responceData: any = null;

  getbaseurl() {
    return this._baseUrl;
  }

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private cookieService:CookieService,
    private router: Router,
    private _httpClient: HttpClient
    ) 
  {
    if (environment.production) {
      this._baseUrl = baseUrl;
    }
    else {
      this._baseUrl = this.apiURL
    }
  }

  loginUser(formData: AuthRequest): Promise<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Login/Login', formData, { headers })
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  CheckEmailExists(email: string): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Login/CheckEmailExists', { params: { UserEmail: email } })
      .pipe(retry(1), catchError(error => {
        let errorMsg: string;
        if (error.error instanceof ErrorEvent) {
          errorMsg = `Error: ${error.error.message}`;
        } else {
          errorMsg = this.getServerErrorMessage(error);
        }
        return throwError(errorMsg);
      }));
  }

  SaveUsers(formData: UserForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Login/RegisterUsers', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  ResetPassword(formData: ResetPassword): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Login/UpdatePassword', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  GetUserCompanyByID(req: { UserId: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Login/GetUserCompanyByID', { params: req })
      .pipe(retry(1), catchError(error => {
        let errorMsg: string;
        if (error.error instanceof ErrorEvent) {
          errorMsg = `Error: ${error.error.message}`;
        } else {
          errorMsg = this.getServerErrorMessage(error);
        }
        return throwError(errorMsg);
      }));
  }

  logoutUser(){
    this.cookieService.deleteAll();
    this.router.navigate(['']);
  }

  private sidebarVisible = new BehaviorSubject<boolean>(true);
  sidebarVisible$ = this.sidebarVisible.asObservable();

  toggleSidebar() {
    this.sidebarVisible.next(!this.sidebarVisible.value);
  }

  private getServerErrorMessage(error: HttpErrorResponse) {
    switch (error.status) {
      case 404: {
        return error.error.error;
      }
      case 403: {
        return error.error.error;
      }
      case 500: {
        return error.error.error;
      }
      default: {
        return error.error.error;
      }
  
    }
  }
}
