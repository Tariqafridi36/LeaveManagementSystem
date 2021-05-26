import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';
import { AuthGuard } from './_gaurds/auth.guard';

export function token_Getter() {
  return localStorage.getItem('token');
}
const routes: Route[] = [

  { path: '', pathMatch: 'full', redirectTo: 'account' },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule)
  },
  {
    path: 'home',
    canActivate: [AuthGuard],
    loadChildren: () => import('./feature/feature.module').then(m => m.FeatureModule),
  }
   
];


@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
