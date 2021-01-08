import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Login } from '../_models/Login';
import { User } from '../_models/User';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.css']
})
export class ConfirmModalComponent implements OnInit {
  name = '';
  message = '';
  currentUsername = '';
  username = '';

  currentUser = {} as User;

  @Input() confirm = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  confirmUsername(): void
  {
    if (this.username === this.currentUser.username)
    {
      this.confirm.emit();
      this.bsModalRef.hide();
    }
    else
    {
      this.toastr.error('Invalid username');
    }

  }

}
