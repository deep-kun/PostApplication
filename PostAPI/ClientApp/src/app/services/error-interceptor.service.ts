import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from './authentication.service';
import { badRequestResponse } from '../model/badRequestResponse';


@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError((httpResponse: HttpErrorResponse) => {
      if (httpResponse.status === 401) {
        this.authenticationService.logout();
        location.reload(true);
      }


      const error = <badRequestResponse>httpResponse.error;

      console.log('interceptor error = ' + error.errorMessage);
      
      return throwError(error.errorMessage);
    }));
  }
}
