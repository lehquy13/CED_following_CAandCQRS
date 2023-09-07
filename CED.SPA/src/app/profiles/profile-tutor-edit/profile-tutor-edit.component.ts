import {Component, OnInit} from '@angular/core';
import {Tutor} from "../../_models/Tutor";

@Component({
  selector: 'app-profile-tutor-edit',
  templateUrl: './profile-tutor-edit.component.html',
  styleUrls: ['./profile-tutor-edit.component.css']
})
export class ProfileTutorEditComponent implements OnInit {

  tutor: Tutor;
  academicList = [{value: "", displayName: "Academic Level"}, {
    value: "student",
    displayName: "Student"
  }, {value: "graduated", displayName: "Graduated"}, {value: "lecturer", displayName: "Lecturer"}];

  constructor() {
  }

  ngOnInit(): void {
  }
}
