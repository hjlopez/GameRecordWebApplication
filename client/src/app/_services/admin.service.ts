import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<Partial<User[]>>
  {
    return this.http.get<Partial<User[]>>(environment.apiUrl + 'admin/get-users').pipe(
      map((response: Partial<User[]>) => {
        const user = response;
        return user;
      })
    );
  }

  deleteUser(username: string): Observable<any>
  {
    return this.http.delete(environment.apiUrl + 'admin/delete-user/' + username).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

  updateUser(user: User): Observable<any>
  {
    return this.http.put(environment.apiUrl + 'admin/update-user', user).pipe(
      map((response: any) => {
        return response;
      })
    );
  }
}
