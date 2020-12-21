import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { BilliardsTournament } from 'src/app/_models/BilliardsTournament';

@Component({
  selector: 'app-tournament-modal',
  templateUrl: './tournament-modal.component.html',
  styleUrls: ['./tournament-modal.component.css']
})
export class TournamentModalComponent implements OnInit {
  name = '';
  tournament!: BilliardsTournament;
  @Input() confirm = new EventEmitter();
  addTournament = new FormGroup({
    tournamentName: new FormControl()
  });
  editDeleteTournament = new FormGroup({
    id: new FormControl(),
    tournamentName: new FormControl()
  });

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) { }

  ngOnInit(): void {
    if (this.name === 'Add Tournament')
    {
    this.createForm();
    }
    else
    {
      this.editDeleteForm();
    }
  }

  createForm(): void
  {
    this.addTournament = this.fb.group({
      tournamentName: ['', Validators.required]
    });
  }

  editDeleteForm(): void
  {
    this.editDeleteTournament = this.fb.group({
      id: [this.tournament.id],
      tournamentName: [this.tournament.tournamentName, Validators.required]
    });
  }

  submit(form: any): void
  {
    this.confirm.emit(form.value);
    this.bsModalRef.hide();
  }
}
