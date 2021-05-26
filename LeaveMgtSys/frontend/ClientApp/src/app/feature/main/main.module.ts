import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CustomModule } from 'src/app/custom/custom.module';
import { MaterialModule } from 'src/app/material/material.module';
import { ChartsModule } from 'ng2-charts';


@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    CustomModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MainRoutingModule,
    ChartsModule
  ]
})
export class MainModule { }
