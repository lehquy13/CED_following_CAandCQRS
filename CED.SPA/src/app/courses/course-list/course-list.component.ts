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
  courses : any = [];
  pagination: Pagination | any;
  pageNumber = 1;
  pageSize = 5;
  constructor(private courseService: CourseService,private alertify : AlertifyService, private route : ActivatedRoute) {
    this.loadCourses();
  }


  ngOnInit() {

  }
  loadCourses() {
    this.courseService.getCourses(this.pageNumber, this.pageSize).subscribe((res: any) => {
      this.courses = res.result;
      console.log(this.courses);
      this.pagination = res.pagination;
    }, (error) => {
      this.alertify.error(error);
    }
      );
  }
}
