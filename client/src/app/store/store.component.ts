import { Component, OnInit } from '@angular/core';
import { StoreService } from './store.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrl: './store.component.scss'
})
export class StoreComponent implements OnInit {
  products: IProduct[] = []; 
  brands: IBrand[] = [];
  types: IType[] = []; 
  storeParams=  new StoreParams();
  constructor(private storeService:StoreService) { // Replace 'any' with the actual type of your store service
    // Initialization logic can go here
  }
  ngOnInit(): void {
   this.getProducts();
   this.getBrands();
   this.getTypes();
  }

  getProducts(){
      this.storeService.getProducts(this.storeParams).subscribe({

        next: response => this.products = response.data // Assign the fetched products to the component's products property
        , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
        , complete: () => console.log('Product fetch complete') // Optional: Log when the
        
      }); 
  }
  getBrands(){
      this.storeService.getBrands().subscribe({

        next: response => this.brands = response // Assign the fetched products to the component's products property
        , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
        , complete: () => console.log('Product fetch complete') // Optional: Log when the
        
      }); 
  }

  getTypes(){
      this.storeService.getTypes().subscribe({

        next: response => this.types = response // Assign the fetched products to the component's products property
        , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
        , complete: () => console.log('Product fetch complete') // Optional: Log when the
        
      }); 
  }

  onBrandSelected(brandId: string) {
    this.storeParams.brandId = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId: string) {
    this.storeParams.typeId = typeId;
    this.getProducts();
  }
}
