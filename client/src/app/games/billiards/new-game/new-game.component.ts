import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';

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

  winName = '';
  loseName = '';

  constructor(private fb: FormBuilder, private toastr: ToastrService, private billiardsMatch: BilliardsMatchService) { }

  ngOnInit(): void {
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

  change(): void
  {
    this.formEmit.emit(false);
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

    this.newMatch.controls.totalGamesPlayed.setValue(this.newMatch.get('winnerWins')?.value + this.newMatch.get('loserWins')?.value);
    this.newMatch.controls.tournamentId.setValue(this.tournament.id);

    this.billiardsMatch.insertMatch(this.newMatch.value).subscribe(() => {
      this.toastr.success('Match inserted.');
    });

    // this.newMatch.reset();
  }

}
