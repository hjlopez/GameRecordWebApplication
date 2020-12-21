import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { UserEditComponent } from 'src/app/account/user-edit/user-edit.component';
import { BilliardsTournament } from 'src/app/_models/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/BilliardsTournamentMembers';
import { User } from 'src/app/_models/User';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-member-modal',
  templateUrl: './member-modal.component.html',
  styleUrls: ['./member-modal.component.css']
})
export class MemberModalComponent implements OnInit {
  name = '';
  tournament!: BilliardsTournament;
  members: BilliardsTournamentMembers[] = [];
  searchedUser!: User;
  searchedTag = '';
  alreadyMember = false;
  newMember!: BilliardsTournamentMembers;
  @Input() confirm = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private adminService: AdminService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.adminService.getMembers(this.tournament.id).subscribe(list => this.members = list);
  }

  search(): void
  {
    this.adminService.searchMember(this.searchedTag).subscribe(member => {
      this.searchedUser = member;
      if (this.members.filter(x => x.username === this.searchedUser.username).length > 0)
      {
        this.alreadyMember = true;
      }
      else{
        this.alreadyMember = false;
      }
    });
  }

  addMember(): void
  {
    if (this.alreadyMember)
    {
      this.toastr.error('Already a member!');
      return;
    }

    this.confirm.emit({tournamentId: this.tournament.id, userId: this.searchedUser.id});
    this.bsModalRef.hide();
  }

  removeMember(member: BilliardsTournamentMembers): void
  {
    this.confirm.emit(member.id);
    this.bsModalRef.hide();
  }
}
