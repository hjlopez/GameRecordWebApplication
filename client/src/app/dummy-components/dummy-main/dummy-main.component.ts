import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-dummy-main',
  templateUrl: './dummy-main.component.html',
  styleUrls: ['./dummy-main.component.css']
})
export class DummyMainComponent implements OnInit {
  user!: User;

  constructor(private router: Router, private accountService: AccountService) {
    this.accountService.currentUser$.subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    if (this.user)
    {
      this.router.navigateByUrl('user/billiards');
    }
    else
    {
      this.router.navigateByUrl('billiards');
    }
  }

}
