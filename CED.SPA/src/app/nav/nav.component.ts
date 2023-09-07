import { Component, OnInit } from '@angular/core';
import {AuthService} from "../_services/auth.service";
import {AlertifyService} from "../_services/alertify.service";
import {Router} from "@angular/router";
import _default from "../../../../CED.Web/wwwroot/assets/vendor/chart.js/core/core.interaction";
import x = _default.modes.x;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model: any = {};
  photoUrl!: string;
  constructor(public authService : AuthService, private alertify: AlertifyService, private router: Router){
  }
  loggedIn() {
    return this.authService.loggedIn();
  }
  logOut() {

    this.router.navigate(['/home']);
    this.alertify.success('logged out');
    this.authService.logOut();
  }

  ngOnInit(): void {
    this.model = this.authService.decodedToken?.sub ?? 'Guest';
  }
}
