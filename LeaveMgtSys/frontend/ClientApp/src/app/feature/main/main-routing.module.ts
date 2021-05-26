import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';


const routes: Route[] = [
  { path: 'dashboard', component: DashboardComponent }, 
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component : HomeComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
