import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { HttpErrorHandler } from './httpErrorHandler.component';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { of } from "rxjs";
import { map, catchError } from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class AuthorizationFilter implements CanActivate {
  constructor(
    private router: Router,
    private http: HttpClient
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let functionKeys: number[] = [];

    if (!!route.data.functionKeys)
      functionKeys = route.data.functionKeys;

    return this.http.post<boolean>('./Api/Authentication/CanActivate', { functionKeys: functionKeys }).pipe(
      map(result => {
        if (!result)
          this.router.navigateByUrl("Login");
        return result;
      }),
      catchError(httpErrorResponse => {
        if (httpErrorResponse.status === 401)
          this.router.navigateByUrl("Login");
        else
          HttpErrorHandler.Notify(httpErrorResponse);

        return of(false);
      })
    )
  }
};
