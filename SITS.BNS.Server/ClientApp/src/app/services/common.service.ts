import { Inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AddMemberForm, DeleteForm, LeadStatusForm, UpdateHouseForm, UpdateMemberForm, ViewOfficersForm } from '../Datamodels/daraforms';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { leadData } from '../Datamodels/datarequest';

@Injectable({
  providedIn: 'root'
})
export class LeadsService {

  _baseUrl!: string;
  apiURL = environment.rooturl + "/";
  responceData: any = null;

  getbaseurl() {
    return this._baseUrl;
  }

  constructor(
    @Inject('BASE_URL') baseUrl: string,
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

  SaveLeads(formData: leadData): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/SaveLeads', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  UpdateLeadsStatus(formData: LeadStatusForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/UpdateLeadsStatus', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  UpdateAppUsers(formData: ViewOfficersForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/UpdateAppUsers', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  UpdateHouse(formData: UpdateHouseForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/UpdateHouse', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  UpdateMember(formData: UpdateMemberForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/UpdateMember', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  AddMember(formData: AddMemberForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/AddMember', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  DeleteAppUser(id: number): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .delete(this._baseUrl + 'api/Leads/DeleteAppUser/' + id)
        .subscribe(
          (response: any) => {
            resolve(response);
          },
          reject
        );
    });
  }

  GetLeadsByRoles(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetLeadsByRoles', { params: req })
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

  GetFamilyMembers(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetAllMembers', { params: req })
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

  GetAllAids(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetAllAids', { params: req })
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

  GetAllOfficers(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetAllOfficers', { params: req })
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

  GetLeadsDataByID(req: { LeadId: string; UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetLeadsDataByID', { params: req })
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

  GetHousebyID(id: number): Observable<any> {
    return this._httpClient
      .get<any>(`${this._baseUrl}api/Leads/GetHousebyID?id=${id}`)
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

  GetMemberbyID(id: number): Observable<any> {
    return this._httpClient
      .get<any>(`${this._baseUrl}api/Leads/GetMemberbyID?id=${id}`)
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

  DeleteMember(formData: DeleteForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/DeleteMember', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  DeleteHouse(formData: DeleteForm): Promise<any> {
    return new Promise((resolve, reject) => {
      this._httpClient
        .post(this._baseUrl + 'api/Leads/DeleteHouse', formData)
        .subscribe(
          (response: any) => {
            this.responceData = response;
            resolve(this.responceData);
          },
          reject
        );
    });
  }

  GetLeadsByID(req: { LeadId: string; UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetLeadsByID', { params: req })
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

  GetLeadsSubmitterDataByID(req: { LeadId: string; UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetLeadsSubmitterDataByID', { params: req })
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

  GetLeadsDashboard(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetLeadsDashboard', { params: req })
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

  GetDashboardStatus(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetDashboardStatus', { params: req })
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

  GetDashboardPie(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetDashboardPie', { params: req })
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

  GetDashboardBar(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetDashboardBar', { params: req })
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

  GetDashboardMonthStatus(req: { UserId: string; UserRole: string }): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetDashboardMonthStatus', { params: req })
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

  GetAllCompanyIsAvailable(): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Company/GetAllCompanyIsAvailable')
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

  GetAllAuditLogs(): Observable<any> {
    return this._httpClient
      .get<any>(this._baseUrl + 'api/Leads/GetAllAuditLogs')
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
