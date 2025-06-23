import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination'; // Ensure you have ngx-bootstrap installed



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ]
  , exports: [
    CommonModule,
    PaginationModule
  ]
})
export class SharedModule { }
