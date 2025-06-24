import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,RouterModule
  ],
  exports: [NavbarComponent,RouterModule] // Export NavbarComponent so it can be used in other modules
})
export class CoreModule { }
