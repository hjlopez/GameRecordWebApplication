import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { BilliardsMatch } from 'src/app/_models/billiards/BilliardsMatch';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { UserWins } from 'src/app/_models/billiards/UserWins';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';
import { BilliardsSeasonService } from 'src/app/_services/billiards-season.service';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-view-game',
  templateUrl: './view-game.component.html',
  styleUrls: ['./view-game.component.css']
})
export class ViewGameComponent implements OnInit {
  name = '';
  winPhotoUrl = '';
  losePhotoUrl = '';
  matchId = 0;
  members = [] as BilliardsTournamentMembers[];
  match = {} as BilliardsMatch;
  modeList = [] as TournamentModes[];
  typeList = [] as TournamentMatchType[];
  seasonList = [] as Season[];

  winType = 0;
  loseType = 0;
  winSeason = 0;
  loseSeason = 0;

  preCovid = false;

  @Input() confirm = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private matchService: BilliardsMatchService, private accountService: AccountService,
              private seasonService: BilliardsSeasonService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.matchId = this.match.id;
    if (this.name !== 'Delete Match?')
    {
      this.getSeasonTypeMatchup(this.match.winUserId, this.match.loseUserId, 'w');
      this.getSeasonTypeMatchup(this.match.loseUserId, this.match.winUserId, 'l');

      this.getSeasonMatchup(this.match.winUserId, this.match.loseUserId, 'w');
      this.getSeasonMatchup(this.match.loseUserId, this.match.winUserId, 'l');

      if (this.datePipe.transform(this.match.datePlayed, 'yyyy-MM-dd')?.toString() === '0001-01-01')
      {
        this.preCovid = true;
      }
    }
  }

  getSeasonMatchup(userId: number, opponentUserId: number, tag: string): void
  {
    const wins = {} as UserWins;
    wins.userId = userId;
    wins.opponentUserId = opponentUserId;
    wins.seasonNumberId = this.match.seasonNumberId;
    wins.tournamentId = this.match.tournamentId;

    this.matchService.getSeasonMatchUp(wins).subscribe(response => {

      if (tag === 'w')
      {
        this.winSeason = response.userWins;
      }
      else
      {
        this.loseSeason = response.userWins;
      }
    });
  }

  getSeasonTypeMatchup(userId: number, opponentUserId: number, tag: string): void
  {
    // let win = 0;
    const wins = {} as UserWins;
    wins.userId = userId;
    wins.opponentUserId = opponentUserId;
    wins.typeId = this.match.typeId;
    wins.seasonNumberId = this.match.seasonNumberId;
    wins.tournamentId = this.match.tournamentId;

    this.matchService.getSeasonTournamentMatchUp(wins).subscribe(response => {

      if (tag === 'w')
      {
        this.winType = response.userWins;
      }
      else
      {
        this.loseType = response.userWins;
      }
    });
  }

  getNames(userId: number): string
  {
    return this.members.find(x => x.userId === userId)?.username || '';
  }

  getType(typeId: number): string
  {
    return this.typeList.find(x => x.matchTypeId === typeId)?.matchType || '';
  }

  findSeason(seasonNumberId: number): number
  {
    return this.seasonList.find(x => x.id === seasonNumberId)?.seasonNumber || 0;
  }

  findMode(modeId: number): string
  {
    return this.modeList.find(x => x.modeId === modeId)?.modeName || '';
  }

  loadPicture(userId: number): string
  {
    this.accountService.getUserInfo(userId).subscribe(user => {
      return user.photoUrl;
    });

    return '';
  }

  confirmAction(): void
  {
    // delete
    if (this.name === 'Delete Match?')
    {
      if (this.modeList.find(x => x.modeId === this.match.modeId)?.isConsolation ||
          this.modeList.find(x => x.modeId === this.match.modeId)?.isLast)
      {
        this.seasonService.deleteSeasonHistory(this.matchId).subscribe(() => {
          this.matchService.deleteMatch(this.matchId).subscribe(() => {
            this.confirm.emit('Match deleted.');
            this.bsModalRef.hide();
          });
        });
      }
      else
      {
        this.matchService.deleteMatch(this.matchId).subscribe(() => {
          this.confirm.emit('Match deleted.');
          this.bsModalRef.hide();
        });
      }
    }
  }

}
