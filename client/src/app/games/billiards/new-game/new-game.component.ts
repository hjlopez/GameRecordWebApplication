import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { ConfirmModalComponent } from 'src/app/confirm-modal/confirm-modal.component';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { Season } from 'src/app/_models/billiards/Season';
import { SeasonHistory } from 'src/app/_models/billiards/SeasonHistory';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';
import { BilliardsSeasonService } from 'src/app/_services/billiards-season.service';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {
  @Input() tournamentMemberList: BilliardsTournament[] = [];
  @Input() tournament = {} as BilliardsTournament;
  @Input() modeList = [] as TournamentModes[];
  @Input() typeList = [] as TournamentMatchType[];
  @Input() seasonList = [] as Season[];
  @Input() members = [] as BilliardsTournamentMembers[];
  @Output() formEmit = new EventEmitter();

  newMatch = new FormGroup({
    winnerWins: new FormControl(1),
    loserWins: new FormControl(0)
  });

  currentUser = {} as User;
  winName = '';
  loseName = '';

  bsModalRef = {} as BsModalRef;

  constructor(private fb: FormBuilder, private toastr: ToastrService, private billiardsMatch: BilliardsMatchService,
              private modalService: BsModalService, private accountService: AccountService,
              private seasonService: BilliardsSeasonService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;
    });
    this.loadForm();
  }

  getWinner(id: any): void
  {
    const userId = Number(id);
    this.winName = this.members.find(x => x.userId === userId)?.username || '';
  }

  getLoser(id: any): void
  {
    const userId = Number(id);
    this.loseName = this.members.find(x => x.userId === userId)?.username || '';
  }

  loadForm(): void
  {
    this.newMatch = this.fb.group({
      winUserId: [this.members[0]?.username, Validators.required],
      loseUserId: [this.members[0]?.username, Validators.required],
      typeId: ['', Validators.required],
      modeId: ['', Validators.required],
      seasonNumberId: ['', Validators.required],
      tournamentId: ['', Validators.required],
      winnerWins: [1, Validators.minLength(1)],
      loserWins: [0, Validators.minLength(0)],
      totalGamesPlayed: [1, Validators.minLength(0)]
    });

  }

  change(isLast: boolean): void
  {
    this.formEmit.emit({isRetain: false, lastType: isLast});
  }

  insert(): void
  {
    // validations
    if (this.newMatch.get('winUserId')?.value === null || this.newMatch.get('loseUserId')?.value === null
        || this.newMatch.get('typeId')?.value === '' || this.newMatch.get('modeId')?.value === ''
        || this.newMatch.get('seasonNumberId')?.value === '')
    {
      this.toastr.error('All fields are required.');
      return;
    }

    if (this.newMatch.get('winUserId')?.value === this.newMatch.get('loseUserId')?.value)
    {
      this.toastr.error('Cannot be same player.');
      return;
    }

    if (this.newMatch.get('winnerWins')?.value < this.newMatch.get('loserWins')?.value)
    {
      this.toastr.error('Winner wins can\'t be lower.');
      return;
    }

    const modeIndex = Number(this.newMatch.get('modeId')?.value);
    const index = Number(this.newMatch.get('typeId')?.value);
    const type = this.typeList.find(x => x.matchTypeId === index)?.matchType;

    // set ranking if for consolation round
    // if consolation round, it will be the last that the player will play for the season type
    if (this.modeList.find(x => x.modeId === modeIndex)?.isConsolation)
    {
      if (this.inserMatch())
      {
        this.setRank(this.newMatch.get('winUserId')?.value, Number(this.modeList.find(x => x.modeId === modeIndex)?.highestRank));
        this.setRank(this.newMatch.get('loseUserId')?.value, Number(this.modeList.find(x => x.modeId === modeIndex)?.highestRank) + 1);
      }
    }
    // check if mode is final, if final user must enter their password to confirm
    else if (this.modeList.find(x => x.modeId === modeIndex)?.isLast)
    {
      const config: any = {
        class: 'modal-dialog-centered modal-sm',
        initialState: {
          name: 'Important!',
          message: 'The mode selected is tagged as the last. Inserting a new match will tag the type ('
                + type
                + ') as done. You will not be able to add matches for ' + type + ' in the current season unless you delete this entry.'
                + ' Enter your username to continue.',
          currentUser: this.currentUser
        }
      };
      this.bsModalRef = this.modalService.show(ConfirmModalComponent, config);

      this.bsModalRef.content.confirm.subscribe(() => {
        // set season to done if all types are played
        // this.updateSeasonToDone(Number(this.newMatch.get('seasonNumberId')?.value));

        // set rank
        if (this.inserMatch())
        {
          this.setRank(this.newMatch.get('winUserId')?.value, Number(this.modeList.find(x => x.modeId === modeIndex)?.highestRank));
          this.setRank(this.newMatch.get('loseUserId')?.value, Number(this.modeList.find(x => x.modeId === modeIndex)?.highestRank) + 1);
        }
        else
        {

        }
      });

    }

    else
    {
      this.inserMatch();
    }

  }

  inserMatch(): boolean
  {
    let value = true;
    this.newMatch.controls.totalGamesPlayed.setValue(this.newMatch.get('winnerWins')?.value + this.newMatch.get('loserWins')?.value);
    this.newMatch.controls.tournamentId.setValue(this.tournament.id);

    this.billiardsMatch.insertMatch(this.newMatch.value).subscribe((response: any) => {
      if (response !== null)
      {
        this.toastr.error(response);
        value = false;
      }

      this.toastr.success('Match inserted.');
      value = true;
    });

    return value;
  }

  setRank(userId: number, rank: number): void
  {
    const seasonHistory: Partial<SeasonHistory> = {
      seasonNumberId: this.newMatch.get('seasonNumberId')?.value,
      tournamentId: this.tournament.id,
      typeId: this.newMatch.get('typeId')?.value,
      modeId: this.newMatch.get('modeId')?.value,
      userId,
      rank,
      isDone: true
    };

    this.seasonService.insertSeasonHistory(seasonHistory).subscribe(() => {});
  }

  updateSeasonToDone(seasonNumberId: number): void
  {
    const season: Season = {
      id: this.seasonList.find(x => x.id === seasonNumberId)?.id || 0,
      isDone: true,
      seasonNumber: this.seasonList.find(x => x.id === seasonNumberId)?.seasonNumber || 0,
      tournamentId: this.seasonList.find(x => x.id === seasonNumberId)?.tournamentId || 0,
      tournamentName: ''
    };

    this.seasonService.updateTournamentSeason(season).subscribe(() => {
      this.change(true);
    });
  }

}
