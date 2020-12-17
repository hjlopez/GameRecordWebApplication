import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/User';

@Component({
  selector: 'app-update-user-modal',
  templateUrl: './update-user-modal.component.html',
  styleUrls: ['./update-user-modal.component.css']
})
export class UpdateUserModalComponent implements OnInit {
  user!: User;
  @Input() confirm = new EventEmitter();
  updateInfoForm = new FormGroup({
    id: new FormControl(),
    username: new FormControl(),
    gamerTag: new FormControl(),
    email: new FormControl(),
    playMH: new FormControl(),
    playDota: new FormControl(),
    joinBilliards: new FormControl()
  });

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createForm();
  }

  updateUser(user: FormGroup): void
  {
    user.controls.playMH.setValue(user.get('playMH')?.value === 'no' ? false : true);
    user.controls.playDota.setValue(user.get('playDota')?.value === 'no' ? false : true);
    user.controls.joinBilliards.setValue(user.get('joinBilliards')?.value === 'no' ? false : true);
    user.controls.email.setValue(user.get('email')?.value === '' ? 'sample@gmail.com' : user.get('email')?.value);

    this.confirm.emit(user);
    this.bsModalRef.hide();
  }

  createForm(): void
  {
    this.updateInfoForm = this.fb.group({
      id: [this.user.id],
      username: [this.user.username, Validators.required ],
      gamerTag: [this.user.gamerTag, [Validators.required, Validators.maxLength(20) ] ],
      email: [this.user.email === 'sample@gmail.com' ? '' : this.user.email, Validators.email],
      playMH: [this.user.playMH ? 'yes' : 'no', Validators.required ],
      playDota: [this.user.playDota ? 'yes' : 'no', Validators.required ],
      joinBilliards: [this.user.joinBilliards ? 'yes' : 'no' , Validators.required]
    });
  }
}
