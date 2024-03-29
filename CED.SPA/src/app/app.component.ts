import {Component} from '@angular/core';
import {User} from "./_models/User";
import {JwtHelperService} from "@auth0/angular-jwt";
import {AuthService} from "./_services/auth.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CED.SPA';

  constructor( private authService: AuthService, private jwtHelper: JwtHelperService) {
  }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user') ?? '{}');
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
      //this.authService.changeMemberPhoto(user.photoUrl);
    }
  }
}
