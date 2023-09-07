import {Component, OnInit} from '@angular/core';
import {CourseRequest} from "../../_models/CourseRequest";

@Component({
  selector: 'app-profile-course-request',
  templateUrl: './profile-course-request.component.html',
  styleUrls: ['./profile-course-request.component.css']
})
export class ProfileCourseRequestComponent implements OnInit{
  courseRequests: CourseRequest[];
  constructor() { }

  ngOnInit(): void {

  }
}
