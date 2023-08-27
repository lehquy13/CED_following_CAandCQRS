import { Component } from '@angular/core';
import {AuthService} from "../_services/auth.service";
import {AlertifyService} from "../_services/alertify.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  constructor(public authService : AuthService, private alertify: AlertifyService, private router: Router){}
  loggedIn() {
    return this.authService.loggedIn();
  }
  logOut() {

    this.router.navigate(['/home']);
    this.alertify.success('logged out');
    this.authService.logOut();
  }
}
