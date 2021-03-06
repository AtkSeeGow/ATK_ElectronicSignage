import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LayoutModule } from 'src/app/layout/layout.module'
import { HeartbeatOptionsComponent } from 'src/app/heartbeatOptions/heartbeatOptions.component'

const routes = [
  { path: '', component: HeartbeatOptionsComponent }
];

@NgModule({
  declarations: [
    HeartbeatOptionsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    LayoutModule,
    HttpClientModule,
    FormsModule
  ]
})
export class HeartbeatOptionsModule { }
