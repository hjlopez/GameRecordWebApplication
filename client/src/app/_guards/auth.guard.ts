import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  // using a guard automatically subscribes
  // to not let unauthorize user to access authorize pages
  canActivate(): Observable<boolean>{
    return this.accountService.currentUser$.pipe(
      map((user: User) => {
        if (Object.keys(user).length !== 0) { return true; }
        this.toastr.error('Unathorized!');
        return false;
      })
    );
  }
}
