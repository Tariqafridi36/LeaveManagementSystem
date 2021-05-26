import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { HomeComponent } from '../home/home.component';
import { AuthGuard } from '../_gaurds/auth.guard';


const routes: Routes = [
  {
    path: '', component: MainLayoutComponent,
    children: [ 
      {
        path: 'leave',
        canActivate: [AuthGuard],
        loadChildren: () => import('./leave/leave.module').then(m => m.LeaveModule)
      },
      {
        path: 'main',
        canActivate: [AuthGuard],
        loadChildren: () => import('./main/main.module').then(m => m.MainModule)
      },
      {
        path: 'home',
        pathMatch: 'full',
        redirectTo: 'home'
      },
      { path: 'home', component: HomeComponent },
      { path: '**', component: HomeComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureRoutingModule { }
