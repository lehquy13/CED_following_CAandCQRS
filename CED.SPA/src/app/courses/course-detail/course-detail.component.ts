import { Component } from '@angular/core';
import {CourseService} from "../../_services/course.service";
import {AlertifyService} from "../../_services/alertify.service";
import {ActivatedRoute} from "@angular/router";
import {AuthService} from "../../_services/auth.service";
import {Course} from "../../_models/Course";

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent {
  course: Course;
  constructor(private courseService: CourseService, public authService: AuthService, private alertify : AlertifyService, private route : ActivatedRoute) {
    this.loadCourse();
  }
  loadCourse() {
    this.courseService.getCourse(this.route.snapshot.params['id']).subscribe((courseResponse: Course) => {
      this.course = courseResponse;
      console.log(this.course);
    }, error => {
      this.alertify.error(error);
    });
  }
}
