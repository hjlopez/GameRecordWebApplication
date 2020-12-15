import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ChangePassword } from 'src/app/_models/ChangePassword';
import { AccountService } from 'src/app/_services/account.service';
import { UpdateService } from 'src/app/_services/update.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm = new FormGroup({
    oldPassword: new FormControl(),
    newPassword: new FormControl(),
    retypePassword: new FormControl()
  });
  errorTag = false;
  errorDescriptions: any;

  constructor(private updateService: UpdateService, private toastr: ToastrService, private fb: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.errorTag = false;
    this.createForm();
  }

  createForm(): void
  {
    this.changePasswordForm = this.fb.group({
      oldPassword: ['', Validators.required ],
      newPassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)] ],
      retypePassword: ['', [Validators.required, this.matchValues()]]
    });
  }

  matchValues(): ValidatorFn {
    return (control: AbstractControl) => {
      // control?.value -> confirmPassword control
      // code on the right is the value of the password control
      // if match -> return null, if not pass attach a validator error to fail
      return control?.value === this.changePasswordForm.controls.newPassword.value ? null : {isMatching: true};
    };
  }

  changePassword(): void
  {
    this.updateService.ChangePassword(this.changePasswordForm.value).subscribe(response => {
      this.errorTag = false;
      this.toastr.success(response.message);
      this.router.navigateByUrl('user/billiards');
    }, errors => {
      this.errorTag = true;
      this.errorDescriptions = errors.error;
    });
  }

}
