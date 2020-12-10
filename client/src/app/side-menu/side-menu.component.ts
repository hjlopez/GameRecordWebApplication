import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {
  public showMenu = true;
  showSignIn = false;
  showRegister = false;
  currentUser: any;
  isLogged = false;

  constructor(public accountService: AccountService, public router: Router) { }

  ngOnInit(): void {
  }

  logout(): void
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    this.isLogged = false;
  }

}
