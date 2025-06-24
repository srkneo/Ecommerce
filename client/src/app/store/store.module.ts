import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreComponent } from './store.component';
import { ProductItemsComponent } from './product-items/product-items.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';



@NgModule({
  declarations: [StoreComponent,ProductItemsComponent,ProductDetailsComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [StoreComponent,ProductItemsComponent,ProductDetailsComponent] // Export StoreComponent so it can be used in other modules
})
export class StoreModule { }
