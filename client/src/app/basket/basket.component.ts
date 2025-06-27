import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { IBasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent {
    constructor(public basketService: BasketService) {}

    removeBasketItem(item:IBasketItem){
      this.basketService.removeItemFromBasket(item);
    }

    increamentItem(item:IBasketItem){
      this.basketService.increamentItemQuantity(item);
    }

    decreamentItem(item:IBasketItem){
      this.basketService.decreamentItemQuantity(item);
    }
}
