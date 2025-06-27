import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketServiceService {

  baseurl = 'https://localhost:9010';

  constructor(private http: HttpClient) { }
  private basketSource = new BehaviorSubject<Basket | null>(null);
  baseketSource$ = this.basketSource.asObservable();

  getBasket(username: string) {
    return this.http.get<IBasket>(this.baseurl + '/Basket/GetBasket/mahesh').subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
      },
      error: (error) => {
        console.error('Error fetching basket:', error);
      }
    });
  }

  setbasket(basket: Basket) {
    return this.http.post<IBasket>(this.baseurl + '/Basket/CreateBasket', basket).subscribe({
      next: (basket) => { 
        this.basketSource.next(basket);
      }
    });
  }

  getcurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct,quantity: number = 1) {
    const itemToAdd:IBasketItem = this.mapProductItemToBasketItem(item);
    const basket = this.getcurrentBasket() ?? this.createBasket();

    //now items can be added to the basket
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setbasket(basket);
  }
  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const item = items.find(x => x.productId === itemToAdd.productId);
    if (item) {
      item.quantity += quantity;
      return items;
    }
    items.push({ ...itemToAdd, quantity });
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_username', "mahesh");
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      imageFile: item.imageFile,
    };
  }
}
