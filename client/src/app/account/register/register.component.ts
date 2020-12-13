import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Register } from 'src/app/_models/Register';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // for child to parent communication
  registerModel: Register = new Register();
  registerForm = new FormGroup({
    username: new FormControl(),
    gamerTag: new FormControl(),
    playMH: new FormControl('no'),
    playDota: new FormControl('no'),
    password: new FormControl(),
    confirmPassword: new FormControl()
  });

  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void
  {
    this.registerForm = this.fb.group({
      username: ['', Validators.required ],
      gamerTag: ['', [Validators.required, Validators.maxLength(20) ] ],
      playMH: ['no', Validators.required ],
      playDota: ['no', Validators.required ],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)] ],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });
  }

  sameAsUsername(e: any): void
  {
    // if check box is checked
    if (e.currentTarget.checked && this.registerForm.get('username')?.value !== '')
    {
      this.registerForm.controls.gamerTag.setValue(this.registerForm.get('username')?.value);
    }
    else
    {
      this.registerForm.controls.gamerTag.setValue('');
    }
  }

  cancel(): void
  {
    this.cancelRegister.emit(false);
  }

  register(): void
  {
    // convert to boolean
    this.registerForm.controls.playMH.setValue(this.registerForm.get('playMH')?.value === 'no' ? false : true);
    this.registerForm.controls.playDota.setValue(this.registerForm.get('playDota')?.value === 'no' ? false : true);

    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('user/billiards');
    }, error => {
      error.forEach((message: any) => {
        this.toastr.error(message);
      });
    });

    // this.registerForm.reset();
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      // control?.value -> confirmPassword control
      // code on the right is the value of the password control
      // if match -> return null, if not pass attach a validator error to fail
      return control?.value === this.registerForm.controls.password.value ? null : {isMatching: true};
    };
  }
}
