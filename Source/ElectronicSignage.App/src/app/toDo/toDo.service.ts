import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { ToDo } from '../common/toDo.component';

@Injectable()
export class ToDoService {
  constructor(
    private http: HttpClient
  ) { };

  asyncFetchBy() {
    return this.http.get<any>('Api/ToDo/FetchBy');
  };

  asyncSaveBy(toDo: ToDo) {
    return this.http.post<any>('Api/ToDo/SaveBy', toDo);
  };

  asyncDeleteBy(toDo: ToDo) {
    return this.http.post<any>('Api/ToDo/DeleteBy', toDo);
  };
}