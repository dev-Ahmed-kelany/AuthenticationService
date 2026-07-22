import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';

import { DashboardHome } from './pages/dashboard-home/dashboard-home';
import { Users } from './pages/users/users';
import { Roles } from './pages/roles/roles';
import { Permissions } from './pages/permissions/permissions';
import { Profile } from './pages/profile/profile';
import { LoginHistory } from './pages/login-history/login-history';
import { AuditLogs } from './pages/audit-logs/audit-logs';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full',
  },

  {
    path: 'auth/login',
    component: Login,
  },

  {
    path: 'dashboard',
    component: Dashboard,

    children: [
      {
        path: '',
        component: DashboardHome,
      },

      {
        path: 'users',
        component: Users,
      },

      {
        path: 'roles',
        component: Roles,
      },

      {
        path: 'permissions',
        component: Permissions,
      },

      {
        path: 'profile',
        component: Profile,
      },

      {
        path: 'login-history',
        component: LoginHistory,
      },

      {
        path: 'audit-logs',
        component: AuditLogs,
      },
    ],
  },

  {
    path: '**',
    redirectTo: 'auth/login',
  },
];
