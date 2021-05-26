import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { LeaveAddComponent } from './leave-add/leave-add.component';
import { LeaveListComponent } from './leave-list/leave-list.component';


const routes: Route[] = [
  { path: 'leave-list', component: LeaveListComponent }, 
  { path: 'leave-add', component: LeaveAddComponent }, 
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component : HomeComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveRoutingModule { }
