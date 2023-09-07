import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../_models/User";
import {FileUploader} from 'ng2-file-upload';

import {FormGroup, NgForm} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {AlertifyService} from "../../_services/alertify.service";
import {AuthService} from "../../_services/auth.service";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  @Input() user: any;
  editForm!: NgForm;
  genderList
    = [{value: "Male", displayName: "Male"}, {value: "Female", displayName: "Female"}];
  academicList = [{value: "", displayName: "Academic Level"}, {value: "student", displayName: "Student"}, {
    value: "graduated",
    displayName: "Graduated"
  }, {value: "lecturer", displayName: "Lecturer"}];
  baseUrl = environment.apiUrl;

  uploader: FileUploader;
  hasBaseDropZoneOver: boolean = false;
  hasAnotherDropZoneOver: boolean = false;

  constructor(private alertify: AlertifyService, private authService: AuthService,
              private route: ActivatedRoute) {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'Profile/ChangeAvatar/' + this.authService.decodedToken.unique_name,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: true,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      console.log(response);
      console.log(item);

      if (response) {
        this.user.image = response;

      }
    };

  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }

  updateUser() {
    if (this.authService.updateUser(this.authService.decodedToken.unique_name, this.user)) {
      //TODO this result is a new token which
      this.alertify.success('User updated successfully');
      this.editForm.reset(this.user);
    }
    ;

  }

  ngOnInit(): void {
  }
}
