import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {BehaviorSubject, map} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {User} from "../_models/User";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any = {};
  currentUser!: User | null;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) {
  }

  changeMemberPhoto(photoUrl: string){
    this.photoUrl.next(photoUrl);
  }

  login(model: any){
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((respone: any) =>{
          this.decodedToken = this.jwtHelper.decodeToken(respone.token);
          console.log(this.decodedToken);
          if(respone){
            localStorage.setItem('token', respone.token);
            localStorage.setItem('user', JSON.stringify(respone.user));
            this.currentUser = respone.user;
            this.changeMemberPhoto(this.currentUser!.photoUrl);
          }


        })
      );
  }

  register(user: User){
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    this.decodedToken = null;
    this.currentUser = null;
  }
}
