import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { MatchType } from 'src/app/_models/billiards/MatchType';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';
import { BilliardsSeasonService } from 'src/app/_services/billiards-season.service';
import { BilliardsService } from 'src/app/_services/billiards.service';
import { AddSeasonComponent } from './add-season/add-season.component';

@Component({
  selector: 'app-billiards',
  templateUrl: './billiards.component.html',
  styleUrls: ['./billiards.component.css']
})
export class BilliardsComponent implements OnInit {
  currentUser!: User;
  tournamentList: BilliardsTournament[] = [];
  modeList: TournamentModes[] = [];
  typeList: TournamentMatchType[] = [];
  seasonList: Season[] = [];
  showAddForm = false;
  val = 0;
  currentTournamentSelected = '';
  currentTournamentIdSelected = 0;

  bsModalRef!: BsModalRef;

  constructor(public accountService: AccountService, private billiards: BilliardsMatchService,
              private billiardsService: BilliardsService, private seasonService: BilliardsSeasonService,
              private modalService: BsModalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(users => this.currentUser = users);
    this.loadTournaments();
  }

  loadOtherValues(id: any): void
  {
    const tournamentId = Number(id);
    this.currentTournamentSelected = this.tournamentList.find(x => x.id === tournamentId)?.tournamentName || '';
    this.currentTournamentIdSelected = tournamentId;

    this.billiardsService.getTournamentModesName(tournamentId).subscribe(modes => this.modeList = modes);
    this.billiardsService.getTournamentTypes(tournamentId).subscribe(types => this.typeList = types);
    this.seasonService.getTournamentSeasons(tournamentId).subscribe(seasons => this.seasonList = seasons);

  }

  toggleAddMatch(): void
  {
    this.showAddForm = !this.showAddForm;
  }

  sample(): void
  {
    // console.log('ketsdsadsd');
  }

  loadTournaments(): void
  {
    if (this.currentUser !== undefined)
    {
      if (this.currentUser.username === 'admin')
      {
        this.billiards.getTournaments(0).subscribe(list => {
          this.tournamentList = list;
          this.loadOtherValues(list[0].id);
        });
      }

      // not admin
      else
      {
        this.billiards.getTournaments(this.currentUser.id).subscribe(list => {
          this.tournamentList = list;
          this.loadOtherValues(list[0].id);
        });
      }
    }

    // logged out
    else
    {
      this.billiards.getTournaments(0).subscribe(list => {
          this.tournamentList = list;
          this.loadOtherValues(list[0].id);
      });
    }
  }

  changeStatus(event: any): void
  {
    this.showAddForm = event;
  }

  addSeason(seasonNumber: number): void
  {
    const currentTournamentId = this.currentTournamentIdSelected;
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        seasonNumber,
        currentTournamentId,
        name: 'Add Season'
      }
    };
    this.bsModalRef = this.modalService.show(AddSeasonComponent, config);

    this.bsModalRef.content.confirm.subscribe((season: Season) => {
      this.seasonService.insertTournamentSeason(season).subscribe(() => {
        this.toastr.success('Season' + season.seasonNumber + ' added!');
      });
    });
  }

}
