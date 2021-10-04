import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/shared/models/login';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject = new BehaviorSubject<User>({}as User);
  public currentUser = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();

  private jwtHelper = new   JwtHelperService();
  constructor(private http:HttpClient) { }

  login(login:Login):Observable<boolean>{
    //call the API
    //POST method
    //return JWT if valid
    //save the token in local storage
    return this.http.post(`${environment.apiUrl}`+'account/login',login).pipe(
      map((response:any) =>{
        if (response){
          // take the response and  // save that token in local storage
          localStorage.setItem('token',response.token)
          this.populateUserInfo();
          return true;
        }
        return false
      })
    )
  }

  populateUserInfo(){
    var token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)){
      //decode the token
      const decodedToken = this.jwtHelper.decodeToken(token);
      console.log(decodedToken);
      
      //set current loggin user into observable
      this.currentUserSubject.next(decodedToken);
      //set authentication status to true
      this.isLoggedInSubject.next(true);
    }

  }

  logout(){
    // delete the token
    localStorage.removeItem('token');

    //clear the obserbables
    this.currentUserSubject.next({}as User);
    this.isLoggedInSubject.next(false);

  }
}
