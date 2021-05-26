import { NgModule } from '@angular/core';
import { ApproveRejectComponent } from './approve-reject/approve-reject.component';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
 



@NgModule({
  declarations: [
    ApproveRejectComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule
  ],
  exports: [
    
  ]
})
export class CustomModule { }
