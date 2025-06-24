import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

// Define your app routes here
const routes: Routes = [
   { path: '', component: HomeComponent },
   { path: 'store', loadChildren: () => import('./store/store.module').then(m => m.StoreModule) }, // Lazy load the store module
   { path: '**', redirectTo: '',pathMatch:'full' } // Redirect any unknown paths to home ,
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
