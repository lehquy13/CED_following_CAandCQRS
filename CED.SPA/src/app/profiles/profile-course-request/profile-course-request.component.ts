import {Component, OnInit, TemplateRef} from '@angular/core';
import {CourseRequest} from "../../_models/CourseRequest";
import {CourseService} from "../../_services/course.service";
import {AlertifyService} from "../../_services/alertify.service";
import {AuthService} from "../../_services/auth.service";
import {Router} from "@angular/router";
import {BsModalService} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-profile-course-request',
  templateUrl: './profile-course-request.component.html',
  styleUrls: ['./profile-course-request.component.css']
})
export class ProfileCourseRequestComponent implements OnInit{
  courseRequests: CourseRequest[];
  constructor(private courseService: CourseService,
              private alertify: AlertifyService,
              private authService: AuthService,
              private router: Router,
              private modalService: BsModalService){  }

  ngOnInit(): void {
    this.courseService.getCourseRequests(this.authService.decodedToken.unique_name).subscribe((res: any) => {
      console.log(res);
      this.courseRequests = res;
    });
  }

  // openModal(template: TemplateRef<any>, id: any) {
  //   this.courseService.getLearningCourse(id).subscribe((courseResponse: any) => {
  //     console.log('courseResponse');
  //     console.log(courseResponse);
  //     this.detailCourse = courseResponse;
  //
  //   });
  //   const modalWidth = 'modal-lg' ;
  //   this.modalRef = this.modalService.show(template);
  //   this.modalRef?.setClass(modalWidth);
  //
  // }


}
