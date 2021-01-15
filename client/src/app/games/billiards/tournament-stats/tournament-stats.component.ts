import { Component, Input, OnInit } from '@angular/core';
import { Season } from 'src/app/_models/billiards/Season';
import { SeasonHistory } from 'src/app/_models/billiards/SeasonHistory';
import { SeasonHistoryArray } from 'src/app/_models/billiards/SeasonHistoryArray';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { AccountService } from 'src/app/_services/account.service';
import { BilliardsSeasonService } from 'src/app/_services/billiards-season.service';

@Component({
  selector: 'app-tournament-stats',
  templateUrl: './tournament-stats.component.html',
  styleUrls: ['./tournament-stats.component.css']
})
export class TournamentStatsComponent implements OnInit {
  @Input() typeList = [] as TournamentMatchType[];
  @Input() seasonList = [] as Season[];
  @Input() tournamentId = 0;

  seasonHistoryList = new Array<SeasonHistoryArray>();
  selectedSeason = 0;

  constructor(private seasonService: BilliardsSeasonService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.seasonHistoryList = [];
    this.selectedSeason = this.seasonList[0].id;
    this.changeSeason(this.selectedSeason);
  }

  loopType(): void
  {
    // loop to get the latest season ranking
    this.typeList.forEach(type => {
      this.loadRanks(this.selectedSeason, type.matchTypeId);
    });

  }

  loadRanks(seasonNumberId: number, typeId: number): void
  {
    this.seasonService.getRank(this.tournamentId, seasonNumberId, typeId).subscribe(response => {
      // add history to array
      response.forEach(res => {
        this.seasonHistoryList.push({
          rank: res.rank,
          member: res.username,
          typeId: res.typeId,
          url: res.url,
          wins: res.wins
        });
      });

    });

  }

  changeSeason(seasonNumberId: any): void
  {
    this.selectedSeason = Number(seasonNumberId);
    this.seasonHistoryList = [];
    this.loopType();
  }



}
