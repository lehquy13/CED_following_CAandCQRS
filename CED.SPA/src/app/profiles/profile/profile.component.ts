import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../_services/auth.service";
import {Router} from "@angular/router";
import {AlertifyService} from "../../_services/alertify.service";
import {User} from "../../_models/User";
import {catchError, EMPTY} from "rxjs";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;

  constructor(private authService: AuthService, private alertify: AlertifyService,
              private router: Router) {

  }

  ngOnInit(): void {
    this.authService.getUser(this.authService.decodedToken.unique_name)
      .pipe(catchError(error => {
        this.alertify.error('Login first');
        this.router.navigate(['/home']);
        return EMPTY;
      }))
      .subscribe(
        data => {
          this.user = data;
        });

  }


}
