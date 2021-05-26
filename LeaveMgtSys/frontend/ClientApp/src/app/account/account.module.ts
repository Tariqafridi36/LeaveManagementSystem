import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AccountRoutingModule } from './account-routing.module';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { AccountLayoutComponent } from './account-layout.component';
 


@NgModule({
  declarations: [
    LoginComponent,
    AccountLayoutComponent
    ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    MaterialModule
  ]
})
export class AccountModule {
  constructor() {
    console.log('account module loaded');
  }
 }
