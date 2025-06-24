import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule
  ],
  exports: [HomeComponent] // Export HomeComponent so it can be used in other modules
})
export class HomeModule { }
