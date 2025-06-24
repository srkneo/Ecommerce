import { Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
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
  @ViewChild('search') searchTerm?: ElementRef; // Assuming you have a search input in your template
  products: IProduct[] = []; 
  brands: IBrand[] = [];
  types: IType[] = []; 
  storeParams=  new StoreParams();
  totalCount: number = 0; // Initialize totalCount to zero

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Ascending', value: 'priceAsc' },
    { name: 'Price: Descending', value: 'priceDesc'}
  ];

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

        next: response =>{
          this.products = response.data
          this.totalCount = response.count; 
          this.storeParams.pageNumber = response.pageIndex; // Update the current page number in storeParams
          this.storeParams.pageSize = response.pageSize; // Update the page size in storeParams

        }  // Assign the fetched products to the component's products property
        , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
        , complete: () => console.log('Product fetch complete') // Optional: Log when the
        
      }); 
  }
  getBrands(){
      this.storeService.getBrands().subscribe({

        next: response => this.brands = [{id:'',name:'All'},...response] // Assign the fetched products to the component's products property
        , error: error => console.error('Error fetching products:', error) // Handle any errors that occur during the fetch
        , complete: () => console.log('Product fetch complete') // Optional: Log when the
        
      }); 
  }

  getTypes(){
      this.storeService.getTypes().subscribe({

        next: response => this.types = [{id:'',name:'All'},...response]  // Assign the fetched products to the component's products property
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

  onSortSelected(sort: string) {
    this.storeParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.storeParams.pageNumber !== event.pageNumber) {
      this.storeParams.pageNumber = event.page;

      this.getProducts();
    }
  }

  onSearch() {
    this.storeParams.search = this.searchTerm?.nativeElement.value; // Get the search term from the input element
    this.storeParams.pageNumber = 1; // Reset to the first page when searching
    this.getProducts(); // Fetch products based on the search term
  }

  onReset() {
    if (this.searchTerm) {
      this.searchTerm.nativeElement.value = ''; // Clear the search input
    }
    this.storeParams = new StoreParams(); // Reset store parameters
    this.getProducts(); // Fetch products with reset parameters
  }
}
