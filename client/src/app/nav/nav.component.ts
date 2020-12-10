import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Login } from '../_models/Login';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginModel: Login = new Login();
  showMenu = true;
  showSignIn = false;
  showRegister = false;
  currentUser: any;
  @ViewChild('loginForm') public loginForm!: NgForm;
  // loginForm!: FormGroup;

  constructor(public accountService: AccountService, public router: Router, private toastr: ToastrService) {
               }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;
    });
  }


  logout(): void
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  login(): void
  {
    if (!this.checkForm())
    {
      this.toastr.error('All fields are required!');
      return;
    }

    this.accountService.login(this.loginModel).subscribe(response => {
      this.router.navigateByUrl('welcome');
      this.loginForm.resetForm();
    });
  }

  checkForm(): boolean
  {
    if (this.loginModel.username === '' || this.loginModel.password === '')
    {
      return false;
    }

    return true;
  }

}
