import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-account-layout',
  templateUrl: './account-layout.component.html',
  styleUrls: ['./account-layout.component.css']
})
export class AccountLayoutComponent implements OnInit {
  height = '200px';
  constructor() { }

  ngOnInit(): void {
    this.height = window.innerHeight + 'px';
  }

}
