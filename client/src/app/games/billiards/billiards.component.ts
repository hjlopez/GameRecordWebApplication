import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { BilliardsMatch } from 'src/app/_models/billiards/BilliardsMatch';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/billiards/BilliardsTournamentMembers';
import { MatchParams } from 'src/app/_models/billiards/MatchParams';
import { MatchType } from 'src/app/_models/billiards/MatchType';
import { PageParams } from 'src/app/_models/billiards/PageParams';
import { Pagination } from 'src/app/_models/billiards/Pagination';
import { Season } from 'src/app/_models/billiards/Season';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { UserWins } from 'src/app/_models/billiards/UserWins';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsMatchService } from 'src/app/_services/billiards-match.service';
import { BilliardsSeasonService } from 'src/app/_services/billiards-season.service';
import { BilliardsService } from 'src/app/_services/billiards.service';
import { AddSeasonComponent } from './add-season/add-season.component';
import { NewGameComponent } from './new-game/new-game.component';
import { ViewGameComponent } from './view-game/view-game.component';

@Component({
  selector: 'app-billiards',
  templateUrl: './billiards.component.html',
  styleUrls: ['./billiards.component.css']
})
export class BilliardsComponent implements OnInit {
  @ViewChild('billiardsTabs', {static: true}) billiardsTabs!: TabsetComponent;
  currentUser!: User;
  members = [] as BilliardsTournamentMembers[];
  tournamentList: BilliardsTournament[] = [];
  tournament = {} as BilliardsTournament;
  modeList: TournamentModes[] = [];
  typeList: TournamentMatchType[] = [];
  seasonList: Season[] = [];
  matches = [] as BilliardsMatch[];
  pagination!: Pagination;
  matchParams = {} as MatchParams;

  showAddForm = false;
  selectedValue = 0;
  currentPage = 1;
  size = 6;
  selectedType = 0;
  selectedMode = 0;
  selectedSeason = 0;

  url1 = '';
  url2 = '';

  bsModalRef!: BsModalRef;

  constructor(public accountService: AccountService, private billiards: BilliardsMatchService,
              private billiardsService: BilliardsService, private seasonService: BilliardsSeasonService,
              private modalService: BsModalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(users => this.currentUser = users);
    this.loadTournaments();
    this.billiardsTabs.tabs[0].active = true;
  }


  loadOtherValues(id: any): void
  {
    const tournamentId = Number(id);
    this.tournament = this.tournamentList.find(x => x.id === tournamentId) || {} as BilliardsTournament;
    this.selectedValue = this.tournament.id;

    this.billiardsService.getTournamentModesName(tournamentId).subscribe(modes => this.modeList = modes);
    this.billiardsService.getTournamentTypes(tournamentId).subscribe(types => this.typeList = types);
    this.seasonService.getTournamentSeasons(tournamentId).subscribe(seasons => this.seasonList = seasons);
    this.billiardsService.getMembersTournament(tournamentId).subscribe(members => this.members = members);
    this.loadMatchTournament(tournamentId, this.currentPage);


  }

  loadMatchTournament(tournamentId: number, currentPage: number): void
  {
    this.currentPage = currentPage;
    this.matchParams.pageNumber = currentPage;
    this.matchParams.pageSize = this.size;
    this.matchParams.tournamentId = tournamentId;
    this.matchParams.seasonNumberId = this.selectedSeason;
    this.matchParams.typeId = this.selectedType;
    this.matchParams.modeId = this.selectedMode;

    this.billiards.getFilteredMatch(this.matchParams).subscribe(matches => {
      this.matches = matches.result;
      this.pagination = matches.pagination;
    });
  }

  toggleAddMatch(): void
  {
    this.showAddForm = !this.showAddForm;

  }

  selectTab(event: any): void
  {
    // console.log(event);
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
          if (list.length > 0)
          {
            this.loadOtherValues(list[0].id);
          }
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
    this.showAddForm = event.isRetain;

    // to refresh season list if final
    if (event.lastType)
    {
      this.seasonService.getTournamentSeasons(this.selectedValue).subscribe(seasons => this.seasonList = seasons);
    }
    this.loadMatchTournament(this.selectedValue, this.currentPage);
  }

  addSeason(seasonNumber: number): void
  {
    const currentTournamentId = this.tournament.id;
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
        this.seasonService.getTournamentSeasons(currentTournamentId).subscribe(seasons => this.seasonList = seasons);
      });
    });
  }

  filter(): void
  {
    this.currentPage = 1;
    this.matchParams.pageNumber = 1;
    this.matchParams.pageSize = this.size;
    this.matchParams.tournamentId = this.selectedValue;
    this.matchParams.seasonNumberId = this.selectedSeason;
    this.matchParams.typeId = this.selectedType;
    this.matchParams.modeId = this.selectedMode;

    this.billiards.getFilteredMatch(this.matchParams).subscribe(response => {
      this.matches = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any): void
  {
    this.currentPage = event.page;
    this.loadMatchTournament(this.selectedValue, this.currentPage);
  }

  changeDropDownValue(event: any): void
  {
    const eventName = event.srcElement.name;

    if (eventName === 'types')
    {
      this.selectedType = event.target.value;
    }
    else if (eventName === 'modes')
    {
      this.selectedMode = event.target.value;
    }
    else if (eventName === 'seasons')
    {
      this.selectedSeason = event.target.value;
    }

  }

  viewGame(title: string, match: BilliardsMatch): void
  {
    let config: any;
    if (title === 'Delete Match?')
    {
      config = {
        class: 'modal-dialog-centered modal-sm',
        initialState: {
          match,
          modeList: this.modeList,
          name: title,
        }
      };
    }
    else
    {

      this.accountService.getUserInfo(match.winUserId).subscribe(value => {
        match.winPhotoUrl = value.photoUrl;
      });

      this.accountService.getUserInfo(match.loseUserId).subscribe(value => {
        match.losePhotoUrl = value.photoUrl;
      });

      config = {
        class: 'modal-dialog-centered modal-md',
        initialState: {
          match,
          members: this.members,
          modeList: this.modeList,
          typeList: this.typeList,
          seasonList: this.seasonList,
          name: title,
        }
      };
    }

    this.bsModalRef = this.modalService.show(ViewGameComponent, config);

    this.bsModalRef.content.confirm.subscribe((message: string) => {
      console.log('reload');
      this.loadMatchTournament(this.selectedValue, 1);
    });
  }



  findUser(userId: number): string
  {
    return this.members.find(x => x.userId === userId)?.username || '';
  }

  findSeason(seasonNumberId: number): number
  {
    return this.seasonList.find(x => x.id === seasonNumberId)?.seasonNumber || 0;
  }

  findType(event: any, typeId: number): void
  {
    const name = this.typeList.find(x => x.matchTypeId === typeId)?.matchType || '';

    if (name === '')
    {
      event.target.src = './assets/8 Ball.jpg';
    }
    else
    {
      event.target.src = './assets/' + name + '.jpg';
    }
  }


  findMode(modeId: number): string
  {
    return this.modeList.find(x => x.modeId === modeId)?.modeName || '';
  }
}
