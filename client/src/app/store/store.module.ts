import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreComponent } from './store.component';
import { ProductItemsComponent } from './product-items/product-items.component';



@NgModule({
  declarations: [StoreComponent,ProductItemsComponent],
  imports: [
    CommonModule    
  ],
  exports: [StoreComponent,ProductItemsComponent] // Export StoreComponent so it can be used in other modules
})
export class StoreModule { }
