import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { HeartbeatOptions } from '../common/heartbeatOptions.component';

@Injectable()
export class HeartbeatOptionsService {
  constructor(
    private http: HttpClient
  ) { };

  asyncFetchBy() {
    return this.http.post<any>('Api/Heartbeat/FetchBy', {});
  };

  asyncSaveBy(heartbeatOptions: HeartbeatOptions) {
    return this.http.post<any>('Api/Heartbeat/SaveBy', heartbeatOptions);
  };

  asyncDeleteBy(heartbeatOptions: HeartbeatOptions) {
    return this.http.post<any>('Api/Heartbeat/DeleteBy', heartbeatOptions);
  };
}
