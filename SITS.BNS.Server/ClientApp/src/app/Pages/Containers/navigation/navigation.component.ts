import { Component } from '@angular/core';
import { PageAuthService } from '../../../services/page-auth.service';
import { navItems } from './nav';
import { LoginService } from '../../../services/login.service';
import { ContainerComponent, ShadowOnScrollDirective, SidebarBrandComponent, SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarNavComponent, SidebarToggleDirective, SidebarTogglerDirective } from '@coreui/angular';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';
import { DefaultFooterComponent } from './default-footer/default-footer.component';
import { DefaultHeaderComponent } from './default-header/default-header.component';
import { IconDirective } from '@coreui/icons-angular';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss',
  standalone: true,
  providers: [MessageService],
  imports: [
    SidebarComponent,
    SidebarHeaderComponent,
    SidebarBrandComponent,
    RouterLink,
    IconDirective,
    NgScrollbar,
    SidebarNavComponent,
    SidebarFooterComponent,
    SidebarToggleDirective,
    SidebarTogglerDirective,
    DefaultHeaderComponent,
    ShadowOnScrollDirective,
    ContainerComponent,
    RouterOutlet,
    DefaultFooterComponent,
    ToastModule
  ]
})
export class NavigationComponent {

  sidebarVisible: boolean = true;
  public navItems = navItems;
  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor(
    private pageAuthService: PageAuthService,
    private loginService: LoginService
  ) { }

  ngOnInit(): void {
    this.navItems = this.pageAuthService.updateNavItems(this.navItems);
    this.loginService.sidebarVisible$.subscribe(
      visible => this.sidebarVisible = visible
    );
  }

  onScrollbarUpdate($event: any) {
    
  }

  logout() {
    this.loginService.logoutUser();
  }
}
