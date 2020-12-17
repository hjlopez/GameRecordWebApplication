import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AdminService } from 'src/app/_services/admin.service';
import { DeleteUserModalComponent } from '../delete-user-modal/delete-user-modal.component';
import { UpdateUserModalComponent } from '../update-user-modal/update-user-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  userList: Partial<User[]> = [];
  bsModalRef!: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(): void
  {
    this.adminService.getAllUsers().subscribe(users => {
      this.userList = users;
    });

  }

  openEditModal(user: any): void
  {
    if (user.username === 'admin') { this.toastr.error('You cannot edit the admin!'); return; }

    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        user,
        name: 'Update User'
      }
    };
    this.bsModalRef = this.modalService.show(UpdateUserModalComponent, config);

    // on confirm
    this.bsModalRef.content.confirm.subscribe((userUpdate: any) => {
      this.adminService.updateUser(userUpdate.value).subscribe(() => {
        this.toastr.success('User updated!');
        this.getAllUsers();
      });
    });
  }


  openDeleteModal(user: any): void
  {
    if (user.username === 'admin') { this.toastr.error('You cannot delete the admin!'); return; }
    // modal configuration properties
    const config: any =  {
      class: 'modal-dialog-centered modal-sm',
      initialState: {
        user,
        name: 'Delete User'
      }
    };
    this.bsModalRef = this.modalService.show(DeleteUserModalComponent, config);

    // on confirm
    this.bsModalRef.content.confirm.subscribe((username: any) => {
      this.adminService.deleteUser(username).subscribe(() => {
        this.toastr.success('User deleted!');
        const index = this.userList.findIndex(x => x?.username === user.username);
        this.userList.splice(index, 1);
      });
    });
  }
}
