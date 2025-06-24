import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { StoreComponent } from './store/store.component';
import { ProductDetailsComponent } from './store/product-details/product-details.component';

// Define your app routes here
const routes: Routes = [
   { path: '', component: HomeComponent },
   { path: 'store', component: StoreComponent },
   { path: 'store/:id', component: ProductDetailsComponent },
   { path: '**', redirectTo: '',pathMatch:'full' } // Redirect any unknown paths to home ,
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
