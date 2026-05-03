import { NgTemplateOutlet, NgStyle } from '@angular/common';
import { Component, input, Input } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AvatarComponent, BadgeComponent, BreadcrumbRouterComponent, ContainerComponent, DropdownComponent, DropdownDividerDirective, DropdownHeaderDirective, DropdownItemDirective, DropdownMenuDirective, DropdownToggleDirective, HeaderComponent, HeaderNavComponent, HeaderTogglerDirective, NavItemComponent, NavLinkDirective, ProgressBarDirective, ProgressComponent, SidebarToggleDirective, TextColorDirective, ThemeDirective } from '@coreui/angular';
import { IconDirective, IconModule, IconSetService } from '@coreui/icons-angular';
import { CookieService } from 'ngx-cookie-service';
import { cilMenu, cilPowerStandby } from '@coreui/icons';
import { LoginService } from '../../../../services/login.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
  styleUrl: './default-header.component.scss',
  standalone: true,
  providers: [IconSetService, ConfirmationService, MessageService],
  imports: [ContainerComponent, ConfirmDialogModule, ToastModule, HeaderTogglerDirective, SidebarToggleDirective, IconModule, IconDirective, HeaderNavComponent, NavItemComponent, NavLinkDirective, RouterLink, RouterLinkActive, NgTemplateOutlet, BreadcrumbRouterComponent, ThemeDirective, DropdownComponent, DropdownToggleDirective, TextColorDirective, AvatarComponent, DropdownMenuDirective, DropdownHeaderDirective, DropdownItemDirective, BadgeComponent, DropdownDividerDirective, ProgressBarDirective, ProgressComponent, NgStyle]
})
export class DefaultHeaderComponent extends HeaderComponent {

  Dashboard: string = '/dashboard';
  Username: string = '';

  constructor(
    private cookieService: CookieService, 
    private router: Router, 
    public iconSet: IconSetService,
    private loginService: LoginService,
    private confirmationService: ConfirmationService, 
    private messageService: MessageService
  ) {
    super();
    this.iconSet.icons = { cilMenu, cilPowerStandby };
  }

  ngOnInit(): void {
    this.Username = this.cookieService.get('e_name');
  }

  toggleSidebar() {
    this.loginService.toggleSidebar();
  }

  async logout() {
    this.cookieService.deleteAll();
    this.router.navigate(['']);
  }

  confirm(event: Event) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Are you sure that you want to proceed ?',
        header: 'Log Out',
        acceptIcon:"none",
        rejectIcon:"none",
        acceptButtonStyleClass:"p-button-contrast custom-accept-btn",
        rejectButtonStyleClass:"p-button-contrast custom-accept-btn",
        accept: () => {
            this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'You have accepted' });
            this.logout();
        },
        reject: () => {
            this.messageService.add({ severity: 'success', summary: 'Not Logout', detail: 'You are online', life: 2000 });
        }
    });
  }
}
