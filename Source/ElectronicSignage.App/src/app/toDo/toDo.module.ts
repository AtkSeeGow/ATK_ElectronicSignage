import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AngularMyDatePickerModule } from 'angular-mydatepicker';

import { LayoutModule } from 'src/app/layout/layout.module'
import { ToDoComponent } from 'src/app/toDo/toDo.component'

const routes = [
  { path: '', component: ToDoComponent }
];

@NgModule({
  declarations: [
    ToDoComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    LayoutModule,
    AngularMyDatePickerModule,
    HttpClientModule,
    FormsModule
  ]
})
export class ToDoModule { }
