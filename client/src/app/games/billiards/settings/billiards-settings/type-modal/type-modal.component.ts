import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MatchType } from 'src/app/_models/billiards/MatchType';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { BilliardsService } from 'src/app/_services/billiards.service';

@Component({
  selector: 'app-type-modal',
  templateUrl: './type-modal.component.html',
  styleUrls: ['./type-modal.component.css']
})
export class TypeModalComponent implements OnInit {
  id = 0;
  name = '';
  tournamentName = '';
  matchType!: MatchType;
  tournamentTypes: TournamentMatchType[] = [];
  searchType = '';

  @Input() confirm = new EventEmitter();
  modalForm = new FormGroup({
    id: new FormControl(),
    type: new FormControl()
  });
  tournamentForm = new FormGroup({
    id: new FormControl(),
    tournamentId: new FormControl(),
    matchTypeId: new FormControl()
  });

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private billiardsService: BilliardsService) { }

  ngOnInit(): void {
    if (this.tournamentName === '')
    {
      this.createForm();
    }
    else{
      this.getTournamentTypes();
    }
  }

  getTournamentTypes(): void
  {
    this.billiardsService.getTournamentTypes(this.id).subscribe(types => this.tournamentTypes = types);
  }

  createForm(): void
  {
    this.modalForm = this.fb.group({
      id: [this.name === 'Add Type' ? 0 : this.matchType.id],
      type: [this.name === 'Add Type' ? '' : this.matchType.type, Validators.required]
    });
  }

  submit(): void
  {
    this.confirm.emit(this.modalForm.value);
    this.bsModalRef.hide();
  }

  addToTournament(): void
  {
    this.billiardsService.getType(this.searchType).subscribe(type => {
      if (type != null)
      {
        const tourType: Partial<TournamentMatchType> = {};
        tourType.matchTypeId = type.id;
        tourType.tournamentId = this.id;

        this.billiardsService.insertTournamentTypes(tourType).subscribe(() => {
          this.confirm.emit('Type added to tournament!');
          this.getTournamentTypes();
          this.searchType = '';
        });
      }
    });
  }

  deleteFromTournament(id: number): void
  {
    this.billiardsService.deleteTournamentType(id).subscribe(() => {
      this.confirm.emit('Removed from tournament!');
      this.getTournamentTypes();
    });
  }
}
