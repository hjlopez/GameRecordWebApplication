import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { LoggedOutComponent } from '../home/logged-out/logged-out.component';
import { Games } from '../_models/Games';
import { Login } from '../_models/Login';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';
import { GamesService } from '../_services/games.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginModel: Login = new Login();
  showMenu = false;
  showRegister = false;
  currentUser!: User;
  @ViewChild('loginForm') public loginForm!: NgForm;
  @Input() out: LoggedOutComponent = new LoggedOutComponent();
  isAdmin = false;
  gameList!: Games[];
  // loginForm!: FormGroup;

  constructor(public accountService: AccountService, public router: Router, private toastr: ToastrService,
              private gamesService: GamesService) {
               }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;

      if (user.roles.length > 1) {this.isAdmin = true; }
      else { this.isAdmin = false; }

      // token checker
      const expiry = (JSON.parse(atob(this.currentUser.token.split('.')[1]))).exp;

      if ((Math.floor((new Date()).getTime() / 1000)) >= expiry)
      {
        this.toastr.error('Session expired');
        this.logout();
      }
    });

    this.getGames();
  }

  getGames(): void
  {
    this.gamesService.getGames().subscribe(games => this.gameList = games);
  }

  logout(): void
  {
    this.accountService.logout();
    this.router.navigateByUrl('billiards');
  }

  login(): void
  {
    if (!this.checkForm())
    {
      this.toastr.error('All fields are required!');
      return;
    }

    this.accountService.login(this.loginModel).subscribe(response => {
      this.router.navigateByUrl('user/billiards');
      this.loginForm.resetForm();
      this.ngOnInit();
    });
  }

  checkRole(): void
  {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      if (user.roles.length > 1) {this.isAdmin = true; }
      else { this.isAdmin = false; }
    });
  }

  checkForm(): boolean
  {
    if ((this.loginModel.username === '' || this.loginModel.password === '') ||
        (this.loginModel.username === null || this.loginModel.password === null))
    {
      return false;
    }

    return true;
  }

  cancelRegister(event: boolean): void
  {
    this.showRegister = event;
  }

}
