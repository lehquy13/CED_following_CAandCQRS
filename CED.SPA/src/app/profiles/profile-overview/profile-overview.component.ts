import {Component, Input} from '@angular/core';
import {User} from "../../_models/User";

@Component({
  selector: 'app-profile-overview',
  templateUrl: './profile-overview.component.html',
  styleUrls: ['./profile-overview.component.css']
})
export class ProfileOverviewComponent {
  @Input() user: User;
}
