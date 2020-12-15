import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { take } from 'rxjs/operators';
import { Photo } from 'src/app/_models/Photo';
import { UpdateService } from 'src/app/_services/update.service';

@Component({
  selector: 'app-photo-edit',
  templateUrl: './photo-edit.component.html',
  styleUrls: ['./photo-edit.component.css']
})
export class PhotoEditComponent implements OnInit {
  @Input() user!: User;
  uploader!: FileUploader;
  hasBaseDropzoneOver!: false;
  currentUser!: User;

  constructor(private accountService: AccountService, private toastr: ToastrService, private updateService: UpdateService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropzoneOver = e; // event
  }

  deletePhoto(): void
  {
    this.updateService.deleteUserPhoto().subscribe(response => {
      this.currentUser.photoUrl = '';
      this.accountService.setCurrentUser(this.currentUser);
      this.toastr.success(response.message);
    }, error => {
      this.toastr.error(error.message);
    });
  }

  initializeUploader(): void {
    this.uploader = new FileUploader({
      url: environment.apiUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.currentUser.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      queueLimit: 1
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false; // to not change the CORS config since we have bearer token
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.currentUser.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.currentUser);
        this.toastr.success('Photo updated!');
      }
    };
  }

}
