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
    if (window.location.href.indexOf("Overview") !== -1)
      $('#overview').addClass('active');
    else if(window.location.href.indexOf("Hardware") !== -1)
      $('#hardware').addClass('active');
    else if(window.location.href.indexOf("Software") !== -1)
      $('#software').addClass('active');
    else if(window.location.href.indexOf("Environment") !== -1)
      $('#environment').addClass('active');
    else
      $('#overview').addClass('active');
  }
}
