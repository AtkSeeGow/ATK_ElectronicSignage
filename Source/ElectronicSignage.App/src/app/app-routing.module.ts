import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorizationFilter } from './common/authorizationFilter.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./toDo/toDo.module').then(m => m.ToDoModule)
  },
  {
    path: 'Login',
    pathMatch: 'full',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'ToDo',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./toDo/toDo.module').then(m => m.ToDoModule)
  },
  {
    path: 'HeartbeatOptions',
    canActivate: [AuthorizationFilter],
    pathMatch: 'full',
    loadChildren: () => import('./heartbeatOptions/heartbeatOptions.module').then(m => m.HeartbeatOptionsModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
