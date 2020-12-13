import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
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
    // console.log(user);
    return this.http.put(environment.apiUrl + 'users', user).pipe(
      map((response: any) => {
        // console.log('update service');
        // console.log(response);
        return response;
        this.accountService.setCurrentUser(response);
      })
    );
  }
}
