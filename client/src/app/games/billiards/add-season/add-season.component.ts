import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Season } from 'src/app/_models/billiards/Season';

@Component({
  selector: 'app-add-season',
  templateUrl: './add-season.component.html',
  styleUrls: ['./add-season.component.css']
})
export class AddSeasonComponent implements OnInit {
  seasonNumber = 0;
  currentTournamentId = 0;
  @Input() confirm = new EventEmitter();

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  addSeason(): void
  {
    const season: Partial<Season> = {
      seasonNumber: this.seasonNumber + 1,
      tournamentId: this.currentTournamentId,
      isDone: false
    };

    this.confirm.emit(season);
    this.bsModalRef.hide();
  }

}
