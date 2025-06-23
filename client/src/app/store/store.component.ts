import { Component, OnInit } from '@angular/core';
import { StoreService } from './store.service';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrl: './store.component.scss'
})
export class StoreComponent implements OnInit {
  products: IProduct[] = []; // Replace 'any' with the actual type of your products  
  constructor(private storeService:StoreService) { // Replace 'any' with the actual type of your store service
    // Initialization logic can go here
  }
  ngOnInit(): void {
    this.storeService.getProducts().subscribe({

      next: response => this.products = response.data // Assign the fetched products to the component's products property
      , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
      , complete: () => console.log('Product fetch complete') // Optional: Log when the
      
    }); // Fetch products from the store service
  }
}
