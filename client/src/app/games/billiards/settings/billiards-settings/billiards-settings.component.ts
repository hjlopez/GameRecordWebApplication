import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BilliardsTournament } from 'src/app/_models/billiards/BilliardsTournament';
import { MatchType } from 'src/app/_models/billiards/MatchType';
import { Modes } from 'src/app/_models/billiards/Modes';
import { TournamentMatchType } from 'src/app/_models/billiards/TournamentMatchType';
import { TournamentModes } from 'src/app/_models/billiards/TournamentModes';
import { BilliardsService } from 'src/app/_services/billiards.service';
import { MemberModalComponent } from './member-modal/member-modal.component';
import { ModesModalComponent } from './modes-modal/modes-modal.component';
import { TournamentModalComponent } from './tournament-modal/tournament-modal.component';
import { TypeModalComponent } from './type-modal/type-modal.component';

@Component({
  selector: 'app-billiards-settings',
  templateUrl: './billiards-settings.component.html',
  styleUrls: ['./billiards-settings.component.css']
})
export class BilliardsSettingsComponent implements OnInit {
  isTournament = true;
  isTourMembers = true;
  isType = true;
  isMode = true;
  tournamentList: BilliardsTournament[] = [];
  bsModalRef!: BsModalRef;
  types: MatchType[] = [];
  modes: Modes[] = [];

  constructor(private billiardsService: BilliardsService, private modalService: BsModalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadTournaments();
    this.loadTypes();
    this.loadModes();
  }

  loadTournaments(): void
  {
    this.billiardsService.getTournaments().subscribe(list => {
      this.tournamentList = list;
    });
  }

  loadTypes(): void
  {
    this.billiardsService.getTypes().subscribe(list => this.types = list);
  }

  loadModes(): void
  {
    this.billiardsService.getModes().subscribe(modes => this.modes = modes);
  }

  addType(): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        name: 'Add Type'
      }
    };
    this.bsModalRef = this.modalService.show(TypeModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((matchType: MatchType) => {
      this.billiardsService.insertType(matchType).subscribe(() => {
        this.toastr.success('Type added!');
        this.loadTypes();
      });
    });
  }

  editType(matchType: MatchType): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        matchType,
        name: 'Edit Type'
      }
    };
    this.bsModalRef = this.modalService.show(TypeModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: MatchType) => {
      this.billiardsService.updateType(value).subscribe(() => {
        this.toastr.success('Type edited!');
        this.loadTypes();
      });
    });
  }

  removeType(matchType: MatchType): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        matchType,
        name: 'Delete Type'
      }
    };
    this.bsModalRef = this.modalService.show(TypeModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: MatchType) => {
      this.billiardsService.deleteType(value.id).subscribe(() => {
        this.toastr.success('Type deleted!');
        this.loadTypes();
      });
    });
  }

  tournamentType(id: number): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        id,
        tournamentName: this.tournamentList.find(x => x.id === id)?.tournamentName,
        name: 'Tournament Type'
      }
    };
    this.bsModalRef = this.modalService.show(TypeModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((message: string) => {
      this.toastr.success(message);
    });
  }

  deleteTournamentType(id: number): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        id,
        name: 'Delete Tournament Type'
      }
    };
    this.bsModalRef = this.modalService.show(TypeModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((typeId: number) => {
      this.billiardsService.deleteTournamentType(typeId).subscribe(() => {
        this.toastr.success('Tournament type deleted!');
      });
    });
  }

  addMode(): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        name: 'Add Mode'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: Modes) => {
      this.billiardsService.insertMode(value).subscribe(() => {
        this.toastr.success('Mode added!');
        this.loadModes();
      });
    });
  }

  editMode(mode: Modes): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        mode,
        name: 'Edit Mode'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: Modes) => {
      this.billiardsService.updateMode(value).subscribe(() => {
        this.toastr.success('Mode edited!');
        this.loadModes();
      });
    });
  }

  removeMode(mode: Modes): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        mode,
        name: 'Delete Mode'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: Modes) => {
      this.billiardsService.deleteMode(value.id).subscribe(() => {
        this.toastr.success('Mode deleted!');
        this.loadModes();
      });
    });
  }

  tournamentMode(mode: Modes): void
  {
    const tournaments = this.tournamentList;
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        mode,
        tournaments,
        name: 'Select Tournament'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: string) => {
      this.toastr.success(value);
    });
  }

  addTournamentMode(id: number): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        id,
        name: 'Tournament Mode'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: TournamentModes) => {
      this.billiardsService.insertTournamentModes(value).subscribe(() => {
        this.toastr.success('Tournament mode added!');
      });
    });
  }

  removeTournamentMode(id: number): void
  {
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        id,
        name: 'Remove Tournament Mode'
      }
    };
    this.bsModalRef = this.modalService.show(ModesModalComponent, config);

    this.bsModalRef.content.confirm.subscribe((value: number) => {
      this.billiardsService.deleteTournamentModes(value).subscribe(() => {
        this.toastr.success('Tournament mode deleted!');
      });
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
      this.billiardsService.addTournament(tournamentName).subscribe(() => {
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
      this.billiardsService.editTournament(tournamentName).subscribe(() => {
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
      this.billiardsService.deleteTournament(tournamentName.id).subscribe(() => {
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
      this.billiardsService.addMember(member).subscribe(() => {
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
      this.billiardsService.removeMember(id).subscribe(() => {
        this.toastr.success('User removed!');
      });
    });
  }

}
