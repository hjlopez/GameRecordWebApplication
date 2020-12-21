import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BilliardsTournament } from 'src/app/_models/BilliardsTournament';
import { BilliardsTournamentMembers } from 'src/app/_models/BilliardsTournamentMembers';
import { AdminService } from 'src/app/_services/admin.service';
import { MemberModalComponent } from './member-modal/member-modal.component';
import { TournamentModalComponent } from './tournament-modal/tournament-modal.component';

@Component({
  selector: 'app-billiards-settings',
  templateUrl: './billiards-settings.component.html',
  styleUrls: ['./billiards-settings.component.css']
})
export class BilliardsSettingsComponent implements OnInit {
  isTournament = true;
  isTourMembers = true;
  tournamentList: BilliardsTournament[] = [];
  bsModalRef!: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadTournaments();
  }

  loadTournaments(): void
  {
    this.adminService.getTournaments().subscribe(list => {
      this.tournamentList = list;
    });
  }

  addTournament(): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        name: 'Add Tournament'
      }
    };
    this.bsModalRef = this.modalService.show(TournamentModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((tournamentName: any) => {
      this.adminService.addTournament(tournamentName).subscribe(() => {
        this.toastr.success('Tournament added!');
        this.tournamentList.push(tournamentName);
      });
    });
  }

  editTournament(tournament: BilliardsTournament): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        tournament,
        name: 'Edit Tournament'
      }
    };
    this.bsModalRef = this.modalService.show(TournamentModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((tournamentName: any) => {
      this.adminService.editTournament(tournamentName).subscribe(() => {
        this.toastr.success('Tournament edited!');
        this.loadTournaments();
      });
    });
  }

  deleteTournament(tournament: BilliardsTournament): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        tournament,
        name: 'Delete Tournament'
      }
    };
    this.bsModalRef = this.modalService.show(TournamentModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((tournamentName: any) => {
      this.adminService.deleteTournament(tournamentName.id).subscribe(() => {
        this.toastr.success('Tournament deleted!');
        const index = this.tournamentList.findIndex(x => x?.id === tournament.id);
        this.tournamentList.splice(index, 1);
      });
    });
  }

  editMembers(tournament: BilliardsTournament): void
  {

    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        tournament,
        name: 'Add Member'
      }
    };
    this.bsModalRef = this.modalService.show(MemberModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((member: any) => {
      this.adminService.addMember(member).subscribe(() => {
        this.toastr.success('Member added to Tournament!');
      });
    });
  }

  removeMember(tournament: BilliardsTournament): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        tournament,
        name: 'Remove Member'
      }
    };
    this.bsModalRef = this.modalService.show(MemberModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((id: number) => {
      this.adminService.removeMember(id).subscribe(() => {
        this.toastr.success('User removed!');
      });
    });
  }

}
