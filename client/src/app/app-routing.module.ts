import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { UnAuthenticatedComponent } from './core/un-authenticated/un-authenticated.component';

// Define your app routes here
const routes: Routes = [
   { path: '', component: HomeComponent ,data:{breadcrumb:'Home'} }, // Home route with breadcrumb data
   {path: 'not-found', component: NotFoundComponent }, 
   {path: 'server-error', component: ServerErrorComponent },
   {path: 'un-authenticated', component: UnAuthenticatedComponent },
   { path: 'store', loadChildren: () => import('./store/store.module').then(m => m.StoreModule),data:{breadcrumb:'Store'} }, // Lazy load the store module
   { path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule), data: { breadcrumb: 'Basket' } }, // Lazy load the basket module
   { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule), data: { breadcrumb: 'Account' } },
   { path: '**', redirectTo: '',pathMatch:'full' } // Redirect any unknown paths to home ,
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
