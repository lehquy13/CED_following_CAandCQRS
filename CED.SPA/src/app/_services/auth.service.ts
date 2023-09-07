import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {BehaviorSubject, map, Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {User} from "../_models/User";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  authBaseUrl = environment.apiUrl + 'auth/';
  baseUrl = environment.apiUrl;

  jwtHelper = new JwtHelperService();
  decodedToken: any = {};
  currentUser!: User | null;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) {
  }

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(model: any) {
    return this.http.post(this.authBaseUrl + 'login', model)
      .pipe(
        map((respone: any) => {
          this.decodedToken = this.jwtHelper.decodeToken(respone.token);
          console.log(this.decodedToken);
          if (respone) {
            localStorage.setItem('token', respone.token);
            localStorage.setItem('user', JSON.stringify(respone.user));
            this.currentUser = respone.user;
            this.changeMemberPhoto(this.currentUser!.image);
          }


        })
      );
  }

  register(user: User) {
    return this.http.post(this.authBaseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    this.decodedToken = null;
    this.currentUser = null;
  }

  //Profile component
  getUser(id: any): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'Profile/' + id);
  }

  updateUser(id: any, userDto: User) {
    this.http.put(this.baseUrl + 'Profile/Edit/' + id, userDto, {responseType: 'text'}).pipe().subscribe(res => {
      localStorage.setItem('token', res);
      localStorage.setItem('user', JSON.stringify(res));
    });
    return true;

  }
  changePassword(id: any, model: any) {
    return this.http.put(this.baseUrl + 'Profile/ChangePassword/' + id, model, {responseType: 'text'});
  }
  setMainPhoto(userId: number, id: number) {
    return this.http.post(this.baseUrl + userId + '/photos/' + id + '/SetMain', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + userId + '/photos/' + id + '/DeletePhoto');
  }
}
