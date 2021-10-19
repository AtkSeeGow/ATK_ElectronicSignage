import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorizationFilter } from './common/authorizationFilter.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./overview/overview.module').then(m => m.OverviewModule)
  },
  {
    path: 'Overview',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./overview/overview.module').then(m => m.OverviewModule)
  },
  {
    path: 'Environment',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./environment/environment.module').then(m => m.EnvironmentModule)
  },
  {
    path: 'Hardware',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./hardware/hardware.module').then(m => m.HardwareModule)
  },
  {
    path: 'Software',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./software/software.module').then(m => m.SoftwareModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
