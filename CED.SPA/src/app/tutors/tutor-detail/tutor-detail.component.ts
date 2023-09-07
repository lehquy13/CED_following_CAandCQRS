import { Component } from '@angular/core';
import {AuthService} from "../../_services/auth.service";
import {AlertifyService} from "../../_services/alertify.service";
import {ActivatedRoute} from "@angular/router";
import {Tutor} from "../../_models/Tutor";
import {TutorService} from "../../_services/tutor.service";

@Component({
  selector: 'app-tutor-detail',
  templateUrl: './tutor-detail.component.html',
  styleUrls: ['./tutor-detail.component.css']
})
export class TutorDetailComponent {
  tutor: Tutor;
  majorstring = '';
  constructor(private tutorService: TutorService, public authService: AuthService, private alertify : AlertifyService, private route : ActivatedRoute) {
    this.loadTutor();
  }
  loadTutor() {
    this.tutorService.getTutor(this.route.snapshot.params['id']).subscribe((tutorResponse: Tutor) => {
      console.log(tutorResponse);
      this.tutor = tutorResponse;
      console.log(this.tutor);
      for(let i of this.tutor.majors){
        this.majorstring += i.name + ', ';
      }
      this.majorstring = this.majorstring.substring(0, this.majorstring.length - 2);
    });
  }
}
