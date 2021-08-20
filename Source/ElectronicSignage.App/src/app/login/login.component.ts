import { Component } from '@angular/core';

import { HttpErrorHandler } from '../common/httpErrorHandler.component';

import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  providers: [LoginService]
})
export class LoginComponent {
  account: string = "";
  password: string = "";

  constructor(private loginService: LoginService) {
  };

  loginAction() {
    this.loginService.asyncGenerateToken(this.account, this.password).subscribe(httpResponse => {
      var response = httpResponse;
      window.localStorage.setItem("token", response.token);
      window.location.href = "./ToDo";
    }, httpErrorResponse => { HttpErrorHandler.Notify(httpErrorResponse); });
  }
}
