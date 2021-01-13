import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';

@Component({
  selector: 'app-individual-stats',
  templateUrl: './individual-stats.component.html',
  styleUrls: ['./individual-stats.component.css']
})
export class IndividualStatsComponent implements OnInit {
  @Input() tournamentName = '';
  @Input() members = [] as BilliardsTournamentMembers[];
  @Input() seasonList = [] as Season[];
  @Input() typeList = [] as TournamentMatchType[];
  currentUser = {} as User;
  // @ViewChild('members', {static: true}) membersDropdown!: DropdownComponent;

  allMembers = [] as BilliardsTournamentMembers[];
  newMemberList = [];
  photoUrl = '';
  seasonValue = 0;
  typeValue = 0;
  selectedMember = 0;


  constructor(public accountService: AccountService, private matchService: BilliardsMatchService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => this.currentUser = user);
    this.loadValues();
    this.loadOtherUsers();
  }

  loadValues(): void
  {
    this.seasonValue = this.seasonList[0].id;
    this.typeValue = this.typeList[0].id;
    this.selectedMember = this.members[0].userId;
  }

  getPhotoUrl(): void
  {
    this.members.forEach(member => {
      this.accountService.getUserInfo(member.userId).subscribe(value => {
        member.photoUrl = value.photoUrl;
      });

    });
  }

  loadOtherUsers(): void
  {
    this.getPhotoUrl();
    this.allMembers = this.members;

    if (this.currentUser === undefined || this.currentUser === null || (Object.keys(this.currentUser).length === 0))
    {
      // console.log(this.)
      const s = this.members.filter(x => x.userId !== this.members[0].userId);
      this.members = s;
    }
    else
    {
      const s = this.members.filter(x => x.userId !== this.currentUser.id);
      this.members = s;
      this.selectedMember = this.currentUser.id;
    }
    this.loadAllTimeWins();
  }

  getSelectedUser(): void
  {
    const s = this.allMembers.filter(x => x.userId !== Number(this.selectedMember));
    this.members = s;

    this.loadAllTimeWins();
    this.loadSeasonWins();
    this.loadTypeWins();
    this.loadNonPlayoffWins();
    this.loadPlayoffWins();
  }

  changeCheckboxStatus(box: any): void
  {
    if (box.target.checked)
    {
      if (box.target.name === 'season')
      {
        this.loadSeasonWins();
      }
      else if (box.target.name === 'type')
      {
        this.loadTypeWins();
      }
      else if (box.target.name === 'nonPlayoff')
      {
        this.loadNonPlayoffWins();
      }
      else if (box.target.name === 'playoff')
      {
        this.loadPlayoffWins();
      }
    }

  }

  loadSeasonWins(): void
  {
    this.members.forEach(member => {
      this.matchService.getSeasonWins(this.selectedMember, member.userId, member.tournamentId, this.seasonValue).subscribe(response => {
        member.seasonWins = response.wins;
        member.seasonPlayed = response.totalGamesPlayed;
      });
    });
  }

  loadAllTimeWins(): void
  {
    this.members.forEach(member => {
      this.matchService.getAllTimeWins(this.selectedMember, member.userId, member.tournamentId).subscribe(response => {
        member.wins = response.wins;
        member.totalGamesPlayed = response.totalGamesPlayed;
      });
    });
  }

  loadTypeWins(): void
  {
    this.members.forEach(member => {
      this.matchService.getTypeWins(this.selectedMember, member.userId, member.tournamentId, this.typeValue).subscribe(response => {
        member.typeWins = response.wins;
        member.typePlayed = response.totalGamesPlayed;
      });
    });
  }

  loadNonPlayoffWins(): void
  {
    this.members.forEach(member => {
      this.matchService.getNonPlayoffWins(this.selectedMember, member.userId, member.tournamentId).subscribe(response => {
        member.nonPlayoffWins = response.wins;
        member.nonPlayoffPlayed = response.totalGamesPlayed;
      });
    });
  }

  loadPlayoffWins(): void
  {
    this.members.forEach(member => {
      this.matchService.getPlayoffWins(this.selectedMember, member.userId, member.tournamentId).subscribe(response => {
        member.playoffWins = response.wins;
        member.playoffPlayed = response.totalGamesPlayed;
      });
    });
  }

}
