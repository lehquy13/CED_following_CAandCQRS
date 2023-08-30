import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';

//Added by me
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import {RouterModule} from "@angular/router";
import {appRoot} from "./routes";
import {AuthService} from "./_services/auth.service";
import {JwtModule} from "@auth0/angular-jwt";
import {HttpClientModule} from "@angular/common/http";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {TabsModule} from 'ngx-bootstrap/tabs';
import { RegisterComponent } from './register/register.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { CourseCardComponent } from './courses/course-card/course-card.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { CourseDetailComponent } from './courses/course-detail/course-detail.component';
import { CourseCreateComponent } from './courses/course-create/course-create.component';
import { TutorlistComponent } from './tutors/tutorlist/tutorlist.component';
export function tokenGetter() {
  return localStorage.getItem('token');
}
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    ContactUsComponent,
    CourseCardComponent,
    CourseListComponent,
    CourseDetailComponent,
    CourseCreateComponent,
    TutorlistComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoot),
    HttpClientModule,
    // Start BootstrapModule
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    BrowserAnimationsModule,
    // End BootstrapModule
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'], // ['localhost:5000', 'localhost:5001']
        disallowedRoutes: ['localhost:5000/api/auth'] // ['localhost:5000/api/auth', 'localhost:5001/api/auth']
      }
    }),
  ],
  providers: [
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
