import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreComponent } from './store.component';



@NgModule({
  declarations: [StoreComponent],
  imports: [
    CommonModule
  ],
  exports: [StoreComponent] // Export StoreComponent so it can be used in other modules
})
export class StoreModule { }
