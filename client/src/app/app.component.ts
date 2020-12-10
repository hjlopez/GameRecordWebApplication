import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  users: any;

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  ngOnInit(): void
  {
    this.setCurrentUser();
  }

  setCurrentUser(): void
  {
    const user: User = JSON.parse(localStorage.getItem('user') || '{}'); // if null, it will pass an empty string
    if (Object.keys(user).length !== 0)
    {
      this.accountService.setCurrentUser(user);
    }
  }
}
