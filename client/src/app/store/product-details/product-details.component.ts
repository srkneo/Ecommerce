import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { StoreService } from '../store.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',  
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;

  constructor(private storeService: StoreService, private route: ActivatedRoute) {}

  ngOnInit():void {    
      this.loadProductDetails();    
  }

  loadProductDetails() {
    
    const productId = this.route.snapshot.paramMap.get('id');
    if(productId) {
      this.storeService.getProductById(productId).subscribe({

      next: (product: IProduct) => {
        this.product = product;
      },
      error: (error) => {
        console.error('Error fetching product details:', error);
      }
    });
    }    
  }
}
