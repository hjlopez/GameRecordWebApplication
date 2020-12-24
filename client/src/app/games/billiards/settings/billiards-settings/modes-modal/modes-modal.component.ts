import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { Modes } from 'src/app/_models/billiards/Modes';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { BilliardsService } from 'src/app/_services/billiards.service';

@Component({
  selector: 'app-modes-modal',
  templateUrl: './modes-modal.component.html',
  styleUrls: ['./modes-modal.component.css']
})
export class ModesModalComponent implements OnInit {
  @Input() confirm = new EventEmitter();
  name = '';
  mode!: Modes;
  tournaments: BilliardsTournament[] = [];
  tournamentModes: TournamentModes[] = [];
  selectedTourMode!: TournamentModes;
  tour!: string;
  modeId = 0;

  hideSubmit = false;
  hideEdit = true;
  hideDelete = true;

  modeForm = new FormGroup({
    id: new FormControl(),
    mode: new FormControl()
  });

  tournamentForm = new FormGroup({
    id: new FormControl(),
    tournamentId: new FormControl(),
    order: new FormControl(),
    isLast: new FormControl(),
    isConsolation: new FormControl(),
    highestRank: new FormControl(),
    isPlayoff: new FormControl(),
    modeId: new FormControl()
  });

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private billiardsService: BilliardsService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    if (this.name === 'Select Tournament')
    {
      this.createTournamentForm();
    }
    else
    {
      this.createForm();
    }
  }

  createTournamentForm(): void
  {
    this.tournamentForm = this.fb.group({
      id: [0 ],
      tournamentId: ['' , Validators.required],
      order: [1, [Validators.required, Validators.min(1)]],
      isLast: [false, Validators.required],
      isConsolation: [false, Validators.required],
      highestRank: [1, [Validators.required, Validators.min(1)]],
      isPlayoff: [false, Validators.required],
      modeId: [this.mode.id , Validators.required]
    });
  }

  loadTournamentForm(tournamentId: number): void
  {
    // condition if tournament already has this mode
    this.billiardsService.getTournamentModes(tournamentId).subscribe(modes => {
      this.tournamentModes = modes;

      const currentValue = this.tournamentModes.find(x => x.tournamentId === tournamentId && x.modeId === this.mode.id);

      this.tournamentForm.controls.tournamentId.setValue(tournamentId);
      this.tour = this.tournaments.find(s => s.id === tournamentId)?.tournamentName || '';

      // if there is value, set values
      if (currentValue !== undefined)
      {
        this.hideSubmit = true;
        this.hideEdit = false;
        this.hideDelete = false;

        this.tournamentForm = this.fb.group({
          id: [currentValue.id],
          tournamentId: [tournamentId, Validators.required],
          order: [currentValue.order],
          isLast: [currentValue.isLast],
          isConsolation: [currentValue.isConsolation],
          highestRank: [currentValue.highestRank],
          isPlayoff: [currentValue.isPlayoff],
          modeId: [currentValue.modeId]
        });
      }
      else
      {
        this.hideSubmit = false;
        this.hideEdit = true;
        this.hideDelete = true;
      }
    });

  }

  createForm(): void
  {
    this.modeForm = this.fb.group({
      id: [this.name === 'Add Mode' ? 0 : this.mode.id],
      mode: [this.name === 'Add Mode' ? '' : this.mode.mode, Validators.required]
    });
  }

  confirmTournament(): void
  {
    if (this.tournamentForm.get('tournamentId')?.value === '')
    {
      this.toastr.error('Please select a tournament!');
      return;
    }

    this.billiardsService.insertTournamentModes(this.tournamentForm.value).subscribe(() => {
      this.confirm.emit('Tournament mode added!');
      this.bsModalRef.hide();
    });
  }

  editTournament(): void
  {

  }

  deleteTournament(): void
  {
    this.billiardsService.deleteTournamentModes(this.tournamentForm.get('id')?.value).subscribe(() => {
      this.confirm.emit('Mode deleted from tournament!');
      this.bsModalRef.hide();
    });
  }

  submit(): void
  {
    this.confirm.emit(this.modeForm.value);
    this.bsModalRef.hide();
  }

}
