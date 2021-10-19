import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LayoutModule } from 'src/app/layout/layout.module'
import { HardwareComponent } from 'src/app/hardware/hardware.component'

const routes = [
  { path: '', component: HardwareComponent }
];

@NgModule({
  declarations: [
    HardwareComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    LayoutModule,
    HttpClientModule,
    FormsModule
  ]
})
export class HardwareModule { }
