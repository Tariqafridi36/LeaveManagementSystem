import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';
import { RouterOutlet, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(
    public authService: AuthService,
    private router: Router,
    private _cookieService: CookieService
  ) {
  }

  ngOnInit() {
    try {
      const token = localStorage.getItem('token');
      if (token) {
        this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      } else {
        this.router.navigateByUrl('/');
      }
    } catch (error) {

    }
  }
}
