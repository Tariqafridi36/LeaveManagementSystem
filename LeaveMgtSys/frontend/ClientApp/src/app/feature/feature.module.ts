import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { FeatureRoutingModule } from './feature-routing.module';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { CoreModule } from '../core/core.module';
 
 

 

@NgModule({
  declarations: [
    MainLayoutComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    FeatureRoutingModule
    
  ]
})
export class FeatureModule { }
