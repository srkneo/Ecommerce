import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule
  ],
  exports: [NavbarComponent] // Export NavbarComponent so it can be used in other modules
})
export class CoreModule { }
