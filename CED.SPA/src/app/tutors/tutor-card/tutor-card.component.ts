import {Component, Input} from '@angular/core';
import {Tutor} from "../../_models/Tutor";
@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.css']
})
export class TutorCardComponent {
  @Input() tutor: Tutor;
}
