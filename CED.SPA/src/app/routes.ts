import {Routes} from "@angular/router";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {RegisterComponent} from "./register/register.component";
import {ContactUsComponent} from "./contact-us/contact-us.component";
import {CourseListComponent} from "./courses/course-list/course-list.component";
import {CourseDetailComponent} from "./courses/course-detail/course-detail.component";
import {CourseCreateComponent} from "./courses/course-create/course-create.component";

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
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }

];
