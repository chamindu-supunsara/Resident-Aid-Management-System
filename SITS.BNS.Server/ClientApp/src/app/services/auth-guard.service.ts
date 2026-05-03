import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { PageAuthService } from './page-auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(
    private router: Router, 
    private authService: PageAuthService, 
    private cookieService: CookieService)
  { }

  async canActivate(state: RouterStateSnapshot) {
    const isOTPVerified = this.cookieService.get('e_jwt') !== '';
    const isInApp = this.cookieService.get('e_jwt');

    if (isInApp === "InApp") {
      if (isOTPVerified) {
        const isAuthed = await this.authService.isAuthorizeForModuleAccess(state.url);
        if (isAuthed) {
          return true;
        } else {
          this.router.navigate(['/']);
          return false;
        }
      } else {
        if (state.url !== '/') {
          this.router.navigate(['/']);
        }
        return false;
      }
    } else {
      const isAuthed = await this.authService.isAuthorizeForModuleAccess(state.url);
        if (isAuthed) {
          return true;
        } else {
          this.router.navigate(['/']);
          return false;
        }
    }
  }
}
