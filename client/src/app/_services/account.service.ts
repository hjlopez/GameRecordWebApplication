import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // 1 -> number of previous value that we want to restore
  // only null or current user object
  // reason why it's set as an observable is to be observed by other components or classes in the app
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable(); // $ because it's an observable

  constructor(private http: HttpClient) { }

  // going to receive values from the nav bar form
  login(model: any): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'account/login', model).pipe(
      map((response: any) => {
        const user: User = response;
        if (user)
        {
          this.setCurrentUser(user);
          this.currentUserSource.next(user);

          const user1: User = JSON.parse(localStorage.getItem('user') || '{}');
        }
      })
    );
  }


  logout(): void
  {
    localStorage.removeItem('user');
    this.currentUserSource.next();
  }

  setCurrentUser(user: User): void
  {
    // commented out
    // user.roles = [];
    // const roles = this.getDecodedToken(user.token).role; // the role is not an array if there is only 1 role

    // // check if return is an array
    // Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string): any
  {

  }
}
