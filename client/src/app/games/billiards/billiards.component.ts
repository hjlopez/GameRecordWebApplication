import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-billiards',
  templateUrl: './billiards.component.html',
  styleUrls: ['./billiards.component.css']
})
export class BilliardsComponent implements OnInit {
  currentUser!: User;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(users => this.currentUser = users);
  }

}
