import { Router } from '@angular/router';
import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map, catchError } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";
import { CookieService } from "ngx-cookie-service";


@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseUrl = "";
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  flag = false;
  logOutReason = '';
  sessionExpiryText = 'Your session has been expired, login again.';

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") base: string,
    private _cookieService: CookieService,
    private router: Router
  ) {

    this.decodedToken = this.jwtHelper.decodeToken(
      localStorage.getItem("token")
    );

    if (environment.env === 'local') {
      this.baseUrl = environment.apiUrl + "auth/";
    } else {
      this.baseUrl = base + 'api/auth/';
    }
  }

  login(model: any) {
  
    return this.http.post(this.baseUrl + "login", model);
   
  }

  register(model: any) {
    return this.http.post(this.baseUrl + "register", model);
  }

  loggedIn() {
    const token = localStorage.getItem("token"); 
    return !this.jwtHelper.isTokenExpired(token);
  }

  getActivity() {
    return localStorage.getItem("activities");
  }

  logout() {
    //this._cookieService.deleteAll('userName');
    //this._cookieService.deleteAll('token');
    this.expireSession()
    localStorage.removeItem('userName');
    localStorage.removeItem('token');
    localStorage.removeItem('activities');
    localStorage.removeItem('flatactivity');
    window.location.reload();
    this.router.navigate(['/']);
  }

  expireSession() {
    debugger
    return this.http.get(this.baseUrl + "DoSessionExpire").pipe(
      map((response: any) => {
        return response;
      })
    ).subscribe();
  }
}
