import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {

  isExpireCount = 0;
  constructor(
    public service: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    private _cookieService: CookieService
  ) {

  }

  ngOnInit(): void {

  }

  sessionExpire() {
    if (this.isExpireCount === 0 && this.service.logOutReason === '') {
      this.isExpireCount++;
      this.alertify.sessionExpire('Session Expired', this.service.sessionExpiryText, () => {
        this.service.logout();
        localStorage.removeItem('userName');
        localStorage.removeItem('token');
        localStorage.removeItem("activities");
        localStorage.removeItem("flatactivity");
        window.location.reload();
        this.router.navigateByUrl("/");
      }, () => {
        this.isExpireCount = 0;
      });
      const element: any = document.getElementsByClassName('ajs-cancel')[0];
      element.style.display = 'none';
    }
  }
}
