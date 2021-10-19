import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LayoutModule } from 'src/app/layout/layout.module'
import { SoftwareComponent } from 'src/app/software/software.component'

const routes = [
  { path: '', component: SoftwareComponent }
];

@NgModule({
  declarations: [
    SoftwareComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    LayoutModule,
    HttpClientModule,
    FormsModule
  ]
})
export class SoftwareModule { }
