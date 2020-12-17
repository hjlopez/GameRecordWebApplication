import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { UpdateService } from 'src/app/_services/update.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  user!: User;
  currentUser!: User;
  updateInfoForm = new FormGroup({
    id: new FormControl(),
    username: new FormControl(),
    gamerTag: new FormControl(),
    email: new FormControl(),
    playMH: new FormControl(),
    playDota: new FormControl(),
    joinBilliards: new FormControl()
  });

  constructor(public accountService: AccountService, private fb: FormBuilder, private toastr: ToastrService,
              private updateService: UpdateService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;
      this.createForm();
      this.setValue();
    });
  }

  createForm(): void
  {
    this.updateInfoForm = this.fb.group({
      id: [this.currentUser.id],
      username: ['', Validators.required ],
      gamerTag: ['', [Validators.required, Validators.maxLength(20) ] ],
      email: ['', Validators.email],
      playMH: ['no', Validators.required ],
      playDota: ['no', Validators.required ],
      joinBilliards: [{value: '', disabled: true} , Validators.required]
    });
  }

  setValue(): void
  {
    // get email value
    const setEmail = this.accountService.getDecodedToken(this.currentUser.token).email;

    this.updateInfoForm.controls.username.setValue(this.currentUser.username);
    this.updateInfoForm.controls.gamerTag.setValue(this.currentUser.gamerTag);
    this.updateInfoForm.controls.email.setValue(setEmail === 'sample@email.com' ? '' : setEmail);
    this.checkValue('boolean');
  }

  saveChanges(): void
  {
    // convert to boolean
    this.checkValue('string');
    // () since there is no return
    this.updateService.updateMember(this.updateInfoForm.value).subscribe(response => {
      this.toastr.success('Profile updated successfully!');

      // return to string
      this.checkValue('boolean');

      // remove token
      this.accountService.logout();
      this.accountService.setCurrentUser(response);

      this.ngOnInit();

      // this.updateInfoForm.reset(response); // to reset form at the same time keep changes
    });
  }

  checkValue(type: any): void
  {
    if (type === 'boolean')
    {
      this.updateInfoForm.controls.playMH.setValue(this.currentUser.playMH ? 'yes' : 'no');
      this.updateInfoForm.controls.playDota.setValue(this.currentUser.playDota ? 'yes' : 'no');
      this.updateInfoForm.controls.joinBilliards.setValue(this.currentUser.joinBilliards ? 'yes' : 'no');
    }
    else
    {
      // convert to boolean
      this.updateInfoForm.controls.playMH.setValue(this.updateInfoForm.get('playMH')?.value === 'no' ? false : true);
      this.updateInfoForm.controls.playDota.setValue(this.updateInfoForm.get('playDota')?.value === 'no' ? false : true);
      this.updateInfoForm.controls.joinBilliards.setValue(this.updateInfoForm.get('joinBilliards')?.value === 'no' ? false : true);
    }
  }

}
