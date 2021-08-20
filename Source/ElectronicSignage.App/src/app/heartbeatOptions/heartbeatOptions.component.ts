import { Component } from '@angular/core';
import { HeartbeatOptions } from '../common/heartbeatOptions.component';
import { HttpErrorHandler } from '../common/httpErrorHandler.component';
import { HeartbeatOptionsService } from './heartbeatOptions.service';
declare const $: any;

@Component({
  selector: 'app-heartbeatOptions',
  templateUrl: './heartbeatOptions.html',
  providers: [HeartbeatOptionsService]
})
export class HeartbeatOptionsComponent {
  heartbeatOption: HeartbeatOptions = new HeartbeatOptions();
  heartbeatOptions: HeartbeatOptions[] = [];

  constructor(private heartbeatOptionsService: HeartbeatOptionsService) {
    this.filter();
  };

  filter() {
    const component = this;

    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.heartbeatOptionsService.asyncFetchBy().subscribe(httpResponse => {
      notify.close();
      component.heartbeatOptions = httpResponse;
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }

  save(heartbeatOption: HeartbeatOptions) {
    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.heartbeatOptionsService.asyncSaveBy(heartbeatOption).subscribe(httpResponse => {
      notify.close();
      this.filter();
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }

  delete(heartbeatOption: HeartbeatOptions) {
    const notify = $.notify({ icon: "tim-icons icon-bell-55", message: "Please Wait" }, { type: 'info', delay: 0, placement: { from: 'top', align: 'right' } });

    this.heartbeatOptionsService.asyncDeleteBy(heartbeatOption).subscribe(httpResponse => {
      notify.close();
      this.filter();
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }
}
