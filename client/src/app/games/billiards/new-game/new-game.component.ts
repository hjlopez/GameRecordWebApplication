import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {
  @Input() tournamentMemberList: BilliardsTournament[] = [];
  @Output() formEmit = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
    // console.log(this.tournamentList);
  }

  change(): void
  {
    this.formEmit.emit(false);
  }

}
