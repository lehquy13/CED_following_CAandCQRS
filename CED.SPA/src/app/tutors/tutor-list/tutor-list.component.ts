import {Component, OnInit} from '@angular/core';
import {Tutor} from "../../_models/Tutor";
import {CourseService} from "../../_services/course.service";
import {AlertifyService} from "../../_services/alertify.service";
import {ActivatedRoute} from "@angular/router";
import {TutorService} from "../../_services/tutor.service";
import {Pagination} from "../../_models/pagination";
import {TutorParams} from "../../_models/TutorParams";

@Component({
  selector: 'app-tutor-list',
  templateUrl: './tutor-list.component.html',
  styleUrls: ['./tutor-list.component.css']
})
export class TutorListComponent implements OnInit {
  tutors: Tutor[] = [];
  tutorParams: any = {};
  pagination: Pagination | any;
  pageNumber = 1;
  pageSize = 5;

  genderList = [{value: "", displayName: "Gender"},{value: "male", displayName: "Males"}, {value: "female", displayName: "Females"}];
  academicList = [{value: "", displayName: "Academic Level"}, {value: "student", displayName: "Student"}, {    value: "graduated",    displayName: "Graduated"  }, {value: "lecturer", displayName: "Lecturer"}];

  constructor(private tutorService: TutorService, private alertify: AlertifyService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.loadTutors();

    this.tutorParams.subjectName = "";
    this.tutorParams.pageIndex = 1;
    this.tutorParams.pageSize = 5;
    this.tutorParams.address = "";
    this.tutorParams.gender = "";
    this.tutorParams.birthYear = '';
    this.tutorParams.academicLevel = "";
  }

  loadTutors() {
    this.tutorService.getTutors(this.pageNumber, this.pageSize).subscribe((res: any) => {
        this.tutors = res.result;
        console.log(this.tutors);
        this.pagination = res.pagination;
      }, (error) => {
        this.alertify.error(error);
      }
    );
  }

  search() {
    console.log(this.tutorParams);
    this.tutorService.getTutors(this.tutorParams.pageSize, this.pageSize, this.tutorParams).subscribe((res: any) => {
      this.tutors = res.result;
      this.pagination = res.pagination;
    }, (error) => {
      this.alertify.error(error);
    });
  }
}
