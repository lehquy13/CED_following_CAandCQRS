import {Component, OnInit, TemplateRef} from '@angular/core';
import {Course, LearningCourse} from "../../_models/Course";
import {Router} from "@angular/router";
import {AuthService} from "../../_services/auth.service";
import {AlertifyService} from "../../_services/alertify.service";
import {CourseService} from "../../_services/course.service";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-profile-all-learning-class',
  templateUrl: './profile-all-learning-class.component.html',
  styleUrls: ['./profile-all-learning-class.component.css']
})
export class ProfileAllLearningClassComponent implements  OnInit{
  courses: Course[] = [];
  rateList: any = [{ value:'1', display:'1' }, { value:'2', display:'2' }, { value:'3', display:'3' }, { value:'4', display:'4' }, { value:'5', display:'5' }, ];
  i = 1;

  modalRef?: BsModalRef;
  detailCourse: LearningCourse ;

  constructor(private courseService: CourseService,
              private alertify: AlertifyService,
              private authService: AuthService,
              private router: Router,
               private modalService: BsModalService){  }

  ngOnInit(): void {
     this.courseService.getLearningCourses(this.authService.decodedToken.unique_name).subscribe((res: any) => {
      this.courses = res.result;
    });
  }

  openModal(template: TemplateRef<any>, id: any) {
    this.courseService.getLearningCourse(id).subscribe((courseResponse: any) => {
      console.log('courseResponse');
      console.log(courseResponse);
      this.detailCourse = courseResponse;

    });
    const modalWidth = 'modal-lg' ;
    this.modalRef = this.modalService.show(template);
    this.modalRef?.setClass(modalWidth);

  }

  reviewTutor(){
    let tutorReviewDto = {
      classId : this.detailCourse.id,
      description :this.detailCourse.tutorReviewDto,
      tutorEmail : this.detailCourse.tutorEmail,
      rate : this.detailCourse.rate
    };
    this.courseService.reviewTutor(this.authService.decodedToken.unique_name, tutorReviewDto).subscribe((res: any) => {
      this.courses = res.result;
    });
  }

}
