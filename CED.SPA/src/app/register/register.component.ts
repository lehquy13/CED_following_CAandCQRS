import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators, FormBuilder, ValidationErrors, AbstractControl} from '@angular/forms';
import {AuthService} from "../_services/auth.service";
import {AlertifyService} from "../_services/alertify.service";
import {Router} from "@angular/router";
import {User} from "../_models/User";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  //Form variables
  registerForm: any = FormGroup;
  submitted = false;
  user: any = {};

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private alertify: AlertifyService, private router: Router) {
    this.createRegisterForm();
  }

//Add user form actions
  get f() {
    return this.registerForm.controls;
  }

  ngOnInit() {
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required]],
        gender: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        birthYear: ['', [Validators.required, Validators.min(1960), Validators.max(2017)]],
        address: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
        confirmPassword: ['', Validators.required]
      },
      {validators: this.passwordMatch('password', 'confirmPassword')}
    );

  }

  passwordMatch(password: string, confirmPassword: string) {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const passwordControl = formGroup.get(password);
      const confirmPasswordControl = formGroup.get(confirmPassword);

      if (!passwordControl || !confirmPasswordControl) {
        return null;
      }

      if (
        confirmPasswordControl.errors //&&        !confirmPasswordControl.errors.passwordMismatch
      ) {
        return null;
      }

      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({passwordMismatch: true});
        return {passwordMismatch: true};
      } else {
        confirmPasswordControl.setErrors(null);
        return null;
      }
    };
  }

  register() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      this.user = Object.assign({}, this.registerForm.value);

      this.authService.register(this.user).subscribe(() => {
          this.alertify.success("registration successful")
        },
        error => {
          this.alertify.error(error)
        },
        () => {
          this.authService.login(this.user).subscribe(() => {
              this.alertify.success("login successful")
              this.router.navigate(['/home']);
            },
            error => {
              this.alertify.error(error)
            })
        });
    }

    console.log(this.registerForm.value);
  }


}
