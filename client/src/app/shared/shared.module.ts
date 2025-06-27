import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination'; // Ensure you have ngx-bootstrap installed
import { CarouselModule } from 'ngx-bootstrap/carousel'; // Assuming you have a CarouselModule for carousels
import { OrderSummaryComponent } from './order-summary/order-summary.component';



@NgModule({
  declarations: [OrderSummaryComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule // Assuming you have a CarouselModule for carousels
    
  ]
  , exports: [
    CommonModule,
    PaginationModule,
    CarouselModule,OrderSummaryComponent // Exporting the CarouselModule so it can be used in other modules
  ]
})
export class SharedModule { }
