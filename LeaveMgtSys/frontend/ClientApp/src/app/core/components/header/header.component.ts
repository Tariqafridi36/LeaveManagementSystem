import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
 
 
 

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  loginUser: any;
  userType: any;
  model: any = {};
  decodedToken: any;
  
  constructor(
    public authService: AuthService
    
  ) {
    this.decodedToken = this.authService.decodedToken; 
    this.loginUser = this.decodedToken?.unique_name;
   
  }

  ngOnInit(): void {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() { 
    this.authService.logout();
  }

}
