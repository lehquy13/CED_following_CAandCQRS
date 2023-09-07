import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../_services/auth.service";
import {AlertifyService} from "../../_services/alertify.service";

@Component({
  selector: 'app-profile-change-password',
  templateUrl: './profile-change-password.component.html',
  styleUrls: ['./profile-change-password.component.css']
})
export class ProfileChangePasswordComponent implements OnInit{
  model: any = {};

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.model.currentPassword = '';
    this.model.newPassword = '';
    this.model.confirmedPassword = '';
  }

  changePassword() {
    this.authService.changePassword(this.authService.decodedToken.unique_name,this.model).subscribe(() => {
      this.alertifyService.success('Password changed successfully');
    }, error => {
      this.alertifyService.error(error);
    });
  }
}
