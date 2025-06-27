import { Component } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { IBasketItem } from '../../shared/models/basket';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  constructor(public basketService: BasketService) { }

  getBasketCount(items:IBasketItem[]) {
    return items.reduce((count, item) => count + item.quantity, 0);
  }
}
