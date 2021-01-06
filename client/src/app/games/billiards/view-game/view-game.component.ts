import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { BilliardsMatch } from 'src/app/_models/billiards/BilliardsMatch';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { UserWins } from 'src/app/_models/billiards/UserWins';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';

@Component({
  selector: 'app-view-game',
  templateUrl: './view-game.component.html',
  styleUrls: ['./view-game.component.css']
})
export class ViewGameComponent implements OnInit {
  name = '';
  winPhotoUrl = '';
  losePhotoUrl = '';
  winId = 0;
  loseId = 154;
  matchId = 0;
  members = [] as BilliardsTournamentMembers[];
  match = {} as BilliardsMatch;
  modeList = [] as TournamentModes[];
  typeList = [] as TournamentMatchType[];
  seasonList = [] as Season[];



  @Input() confirm = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private matchService: BilliardsMatchService, private accountService: AccountService) { }

  ngOnInit(): void {
    // const wins = {} as UserWins;
    // wins.userId = this.match.winUserId;
    // wins.opponentUserId = this.match.loseUserId;
    // wins.typeId = this.match.typeId;
    // wins.seasonNumberId = this.match.seasonNumberId;
    // wins.tournamentId = this.match.tournamentId;

    // this.matchService.getSeasonTournamentMatchUp(wins).subscribe(response => {
    //   console.log(response);
    // });
    this.getSeasonTypeMatchup(this.match.winUserId, this.match.loseUserId, 'w');
    this.getSeasonTypeMatchup(this.match.loseUserId, this.match.winUserId, 'l');

    // console.log(this.winId);
    // console.log(this.loseId);
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
      // console.log(response.userWins);
      // return response.userWins;
      // win = response.userWins;

      if (tag === 'w')
      {
        this.winId = response.userWins;
      }
      else
      {
        this.loseId = response.userWins;
      }
    });

    // console.log(win);
    // return win;
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
      this.matchService.deleteMatch(this.matchId).subscribe(() => {
        this.confirm.emit('Match deleted.');
        this.bsModalRef.hide();
      });
    }
  }

}
