import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LayoutModule } from 'src/app/layout/layout.module'
import { EnvironmentComponent } from 'src/app/environment/environment.component'

const routes = [
  { path: '', component: EnvironmentComponent }
];

@NgModule({
  declarations: [
    EnvironmentComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    LayoutModule,
    HttpClientModule,
    FormsModule
  ]
})
export class EnvironmentModule { }
