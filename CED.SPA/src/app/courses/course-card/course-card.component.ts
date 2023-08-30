import {Component, Input} from '@angular/core';
import {User} from "../../_models/User";
import {Course} from "../../_models/Course";

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent {
  @Input() course: Course;
}
