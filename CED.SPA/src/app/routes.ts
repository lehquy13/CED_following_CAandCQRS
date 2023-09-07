import {Routes} from "@angular/router";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {RegisterComponent} from "./register/register.component";
import {ContactUsComponent} from "./contact-us/contact-us.component";
import {CourseListComponent} from "./courses/course-list/course-list.component";
import {CourseDetailComponent} from "./courses/course-detail/course-detail.component";
import {CourseCreateComponent} from "./courses/course-create/course-create.component";
import {TutorRegistrationComponent} from "./tutors/tutor-registration/tutor-registration.component";
import {TutorDetailComponent} from "./tutors/tutor-detail/tutor-detail.component";
import {TutorListComponent} from "./tutors/tutor-list/tutor-list.component";
import {ProfileComponent} from "./profiles/profile/profile.component";

export const appRoot: Routes
  = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'contact-us',
    component: ContactUsComponent
  },
  {
    path: 'course',
    component: CourseListComponent
  },
  {
    path: 'course/:id',
    component: CourseDetailComponent
  },
  {
    path: 'course/create',
    component: CourseCreateComponent
  },
  {
    path: 'tutor',
    component: TutorListComponent
  },
  {
    path: 'tutor/:id',
    component: TutorDetailComponent
  },
  {
    path: 'tutor/registration',
    component: TutorRegistrationComponent
  },
  {
    path: 'profile',
    component: ProfileComponent
  },

  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }

];
