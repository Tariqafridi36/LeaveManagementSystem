 
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AccountLayoutComponent } from './account-layout.component';


const routes: Routes = [
  {
    path: '', component: AccountLayoutComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'login'
      },
      { path: 'login', component: LoginComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
