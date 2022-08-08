import { Component } from '@angular/core';

import { HttpErrorHandler } from '../common/httpErrorHandler.component';

import { DatePickerUtility } from '../common/utility.component'
import { ToDo } from '../common/toDo.component';

import { ToDoService } from './toDo.service';

declare const $: any;

@Component({
  selector: 'app-toDo',
  templateUrl: './toDo.html',
  providers: [ToDoService]
})
export class ToDoComponent {
  DatePickerUtility = DatePickerUtility;

  toDo:ToDo = new ToDo();
  toDos: ToDo[] = [];

  constructor(private toDoService: ToDoService) {
    this.filter();
  };

  filter() {
    const component = this;

    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.toDoService.asyncFetchBy().subscribe(httpResponse => {
      notify.close();
      component.toDos = httpResponse;
      component.toDos.forEach(function (toDo: ToDo) {
        toDo = ToDo.Parse(toDo);
      });
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }

  save(toDo: ToDo) {
    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.toDoService.asyncSaveBy(ToDo.Clone(toDo)).subscribe(httpResponse => {
      notify.close();
      this.filter();
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }

  delete(toDo: ToDo) {
    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.toDoService.asyncDeleteBy(ToDo.Clone(toDo)).subscribe(httpResponse => {
      notify.close();
      this.filter();
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }
}

