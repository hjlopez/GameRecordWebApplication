import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ChangePassword } from '../_models/ChangePassword';
import { User } from '../_models/User';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class UpdateService {
  user!: User;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    });
  }

  updateMember(user: User): Observable<any> {
  return this.http.put(environment.apiUrl + 'users', user).pipe(
    map((response: any) => {
      return response;
    })
  );

  }

  ChangePassword(changePassword: ChangePassword): Observable<any>
  {
    return this.http.post(environment.apiUrl + 'users/change', changePassword);
  }

  deleteUserPhoto(): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'users/delete-photo', {});
  }
}
