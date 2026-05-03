import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { NavigationComponent } from './Pages/Containers/navigation/navigation.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'dashboard',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: NavigationComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        data: {
          title: 'Dashboard'
        },
        loadChildren: () =>
        import('./Pages/dashboard/dashboard.module').then((m) => m.DashboardModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'add-family',
        data: {
          title: 'New Family'
        },
        loadChildren: () =>
        import('./Pages/add-family/add-family.module').then((m) => m.AddFamilyModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'houses',
        data: {
          title: 'Houses'
        },
        loadChildren: () =>
        import('./Pages/houses/houses.module').then((m) => m.HousesModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'members',
        data: {
          title: 'Members'
        },
        loadChildren: () =>
        import('./Pages/members/members.module').then((m) => m.MembersModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'branch-map',
        data: {
          title: 'Branches Map'
        },
        loadChildren: () =>
        import('./Pages/branch-map/branch-map.module').then((m) => m.BranchMapModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'officer-config',
        data: {
          title: 'Officer Config'
        },
        loadChildren: () =>
        import('./Pages/admin/officer-config/officer-config.module').then((m) => m.OfficerConfigModule),
        canActivate: [AuthGuardService]
      },
      {
        path: 'audit-log',
        data: {
          title: 'Audit Log'
        },
        loadChildren: () =>
        import('./Pages/admin/audit-log/audit-log.module').then((m) => m.AuditLogModule),
        canActivate: [AuthGuardService]
      }
    ]
  },
  { 
    path: '**', 
    component: LoginComponent ,
    data: {
      title: 'Login Page'
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
