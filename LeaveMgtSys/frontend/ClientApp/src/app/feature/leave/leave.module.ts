import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { LeaveRoutingModule } from './leave-routing.module';
import { LeaveListComponent } from './leave-list/leave-list.component';
import { LeaveAddComponent } from './leave-add/leave-add.component';
import { CustomModule } from 'src/app/custom/custom.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material/material.module';
 


@NgModule({
  declarations: [LeaveListComponent, LeaveAddComponent],
  imports: [
    CommonModule,
    CustomModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule, 
    LeaveRoutingModule,
  ]
})
export class LeaveModule { }
