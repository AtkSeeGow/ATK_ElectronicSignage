import { Component, AfterViewInit, AfterViewChecked, NgModule } from '@angular/core';

declare const $: any;

@Component({
  selector: 'layout',
  templateUrl: './layout.html',
  styleUrls: ['./layout.css']
})
export class LayoutComponent implements AfterViewInit {

  constructor() { }

  ngAfterViewInit() {
    if (window.location.href.indexOf("ToDo") !== -1)
      $('#toDo').addClass('active');
    else if (window.location.href.indexOf("HeartbeatOptions") !== -1)
      $('#heartbeatOptions').addClass('active');
    else
      $('#toDo').addClass('active');
  }
}
