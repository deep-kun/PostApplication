import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../model/user';
import { authRepsonse } from '../model/authRepsonse';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private _currentUserSubject: BehaviorSubject<User>;
  //private currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this._currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    //this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    console.log(`try to get = ${JSON.stringify(this.currentUserSubject.value)}`);
    return this.currentUserSubject.value;
  }

  public get currentUserSubject(): BehaviorSubject<User>{
    return this._currentUserSubject;
  }

login(login: string, password: string) {
    return this.http.post<authRepsonse>(environment.apiUrl + `api/auth/authenticate`, { login, password })
        .pipe(map(res => {
            res.user.token = res.token;
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.removeItem('currentUser');
            localStorage.setItem('currentUser', JSON.stringify(res.user));
            this._currentUserSubject.next(res.user);
            return res.user;
        }));
}

register(login: string, password: string, name: string){
  return this.http.post<authRepsonse>(environment.apiUrl + `api/auth/reg`, { login, password, name })
  .pipe(map(res => {
      res.user.token = res.token;
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.removeItem('currentUser');
      localStorage.setItem('currentUser', JSON.stringify(res.user));
      this._currentUserSubject.next(res.user);
      return res.user;
    }));
}

logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this._currentUserSubject.next(null);
}
}
