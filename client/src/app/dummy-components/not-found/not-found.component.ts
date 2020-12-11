import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent implements OnInit {
  user!: User;

  constructor(private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => this.user = user);
  }

  backToHome(): void
  {
    if (this.user) { this.router.navigateByUrl('user/billiards'); }
    else { this.router.navigateByUrl('billiards'); }
  }

}
