import {Component, OnInit} from '@angular/core';
import {AlertifyService} from "../../_services/alertify.service";
import {ActivatedRoute} from "@angular/router";
import {CourseService} from "../../_services/course.service";
import {Pagination} from "../../_models/pagination";

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: any = [];
  courseParams: any = {};
  pagination: Pagination | any;
  pageNumber = 1;
  pageSize = 5;

  constructor(private courseService: CourseService, private alertify: AlertifyService, private route: ActivatedRoute) {
  }


  ngOnInit() {
    this.loadCourses();
    this.courseParams.subjectName = '';
  }

  loadCourses() {
    this.courseService.getCourses(this.pageNumber, this.pageSize).subscribe((res: any) => {
        this.courses = res.result;
        this.pagination = res.pagination;
      }, (error) => {
        this.alertify.error(error);
      }
    );
  }
  search() {
    this.courseService.getCourses(this.pageNumber, this.pageSize, this.courseParams).subscribe((res: any) => {
        this.courses = res.result;
        this.pagination = res.pagination;
      }, (error) => {
        this.alertify.error(error);
      }
    );
  }

}
