import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Validators, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  jwtHelper = new JwtHelperService();

  form: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)])

  });
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    private _cookieService: CookieService
  ) { }

  ngOnInit(): void {
    this._cookieService.delete('userName');
    this._cookieService.delete('token');
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  login(model) {
    this.authService.login(model).subscribe((user?: any) => {
      debugger
      if (user.code === '00') { 
        
        this.authService.decodedToken = this.jwtHelper.decodeToken(user.token); 
        localStorage.setItem('token', user.token);
        
            this.router.navigateByUrl('/home/main/dashboard');
         
          this.alertify.success(user.desc);
        
      } else {
        this.alertify.error(user.desc);
      }
    }, error => {
      this.alertify.error(error);
    });
  }


}
