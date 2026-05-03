import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class PageAuthService {

  constructor(private cookieService: CookieService) { }

  async isAuthorizeForModuleAccess(active_route: any) {
    var roles = JSON.parse(this.cookieService.get('e_user_roles')) as string[];
    const routePath = '/' + (active_route?.[0]?.path || '');

    if (roles.length > 0) {
      if (routePath === '/dashboard' && roles.some(role => ["Admin", "User"].includes(role))) { return true; }
      else if (routePath === '/add-family' && roles.some(role => ["User"].includes(role))) { return true; } 
      else if (routePath === '/houses' && roles.some(role => ["Admin", "User"].includes(role))) { return true; } 
      else if (routePath === '/members' && roles.some(role => ["Admin", "User"].includes(role))) { return true; }
      else if (routePath === '/branch-map' && roles.some(role => ["Admin", "User"].includes(role))) { return true; }
      else if (routePath === '/admin' && roles.some(role => ["Admin"].includes(role))) { return true; }
      else if (routePath === '/officer-config' && roles.some(role => ["Admin"].includes(role))) { return true; }
      else if (routePath === '/audit-log' && roles.some(role => ["Admin"].includes(role))) { return true; }
      else { return false;}
    } else {
      return false;
    }
  }

  updateNavItems(navItems: INavData[]) {
    var roles = JSON.parse(this.cookieService.get('e_user_roles')) as string[];

    if (roles.length > 0) {

      const USER: any[] = [
        '/dashboard', '/add-family', '/members', '/houses','/branch-map'
      ];

      const ADMIN: any[] = [
        '/dashboard',, '/members', '/houses', '/branch-map', '/admin', '/officer-config', '/audit-log'
      ];

      if (roles.some(role => ["User"].includes(role))) {
        return navItems.filter(item => USER.includes(item.url));
      }
      else if (roles.some(role => ["Admin"].includes(role))) {
        return navItems.filter(item => ADMIN.includes(item.url));
      }
      else { return []; }
    } else {
      return [];
    }
  }

}
