import { Component, Input, input } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-items',
  templateUrl: './product-items.component.html',
  styleUrl: './product-items.component.scss'
})
export class ProductItemsComponent {
   @Input() product?: IProduct;

   constructor(private basketService: BasketService) {}

   addItemToBasket() {
     if (this.product) {
       this.basketService.addItemToBasket(this.product);
     }
   }

}
