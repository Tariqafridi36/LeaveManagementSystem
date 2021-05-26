import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap';
import { AuthService } from '../_services/auth.service';
import { MaterialModule } from '../material/material.module';
 

@NgModule({
  declarations: [
    HeaderComponent, 
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    MaterialModule,
    BsDropdownModule.forRoot(),
   
  ],
  exports: [
    HeaderComponent
    
  ],
  providers: [
    AuthService
  ]
})
export class CoreModule { }
