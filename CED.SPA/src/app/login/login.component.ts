import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators, FormBuilder} from '@angular/forms';
import {AuthService} from "../_services/auth.service";
import {AlertifyService} from "../_services/alertify.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  //Form variables
  registerForm: any = FormGroup;
  submitted = false;
  model: any = {};

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private alertify: AlertifyService, private router: Router) {
    this.createRegisterForm();
  }

//Add user form actions
  get f() {
    return this.registerForm.controls;
  }

  ngOnInit() {  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]]
      }
    );
  }

  login() {
    if (this.registerForm.valid) {
      this.model = {
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      }
      this.authService.login(this.model).subscribe(next => {
          this.alertify.success('logged');
        },
        error => {
          this.alertify.error('logged fail');

        },
        () => {
          this.router.navigate(['/home']);

        }
      )
    }
  }

}
