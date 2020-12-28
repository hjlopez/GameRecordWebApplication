import { Component, OnInit } from '@angular/core';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';
import { BilliardsService } from 'src/app/_services/billiards.service';

@Component({
  selector: 'app-billiards',
  templateUrl: './billiards.component.html',
  styleUrls: ['./billiards.component.css']
})
export class BilliardsComponent implements OnInit {
  currentUser!: User;
  tournamentList: BilliardsTournament[] = [];
  showMenu = false;

  constructor(public accountService: AccountService, private billiards: BilliardsMatchService,
              private billiardsService: BilliardsService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(users => this.currentUser = users);
    this.loadTournaments();
  }

  loadTournaments(): void
  {
    if (this.currentUser !== undefined)
    {
      if (this.currentUser.username === 'admin')
      {
        this.billiardsService.getTournaments().subscribe(list => this.tournamentList = list);
        return;
      }

      this.billiards.getTournaments(this.currentUser.id).subscribe(list => {
        this.tournamentList = list;
      });
    }
    else
    {
      this.billiardsService.getTournaments().subscribe(list => this.tournamentList = list);
    }
  }

}
