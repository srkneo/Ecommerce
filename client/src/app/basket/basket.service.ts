import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseurl = 'http://localhost:8010';

  constructor(private http: HttpClient) { }
  private basketSource = new BehaviorSubject<Basket | null>(null);
  baseketSource$ = this.basketSource.asObservable();

  private basketTotal = new BehaviorSubject<IBasketTotal | null>(null);
  basketTotal$ = this.basketTotal.asObservable();

  getBasket(username: string) {
    return this.http.get<IBasket>(this.baseurl + '/Basket/GetBasket/mahesh').subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
        this.calculateBasketTotal();
      },
       error: (error) => {
      if (error.status === 404) {
        // Handle not found basket — treat as empty
        this.basketSource.next(null);
        this.basketTotal.next(null);
        localStorage.removeItem('basket_username');
      } else {
        console.error('Error fetching basket:', error);
      }
    }
    });
  }

  setbasket(basket: Basket) {
    return this.http.post<IBasket>(this.baseurl + '/Basket/CreateBasket', basket).subscribe({
      next: (basket) => { 
        this.basketSource.next(basket);
        this.calculateBasketTotal();
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

  increamentItemQuantity(item:IBasketItem){
    const basket = this.getcurrentBasket();
    if(!basket) return;

    const foundItemIndex = basket.items.findIndex((x) => x.productId === item.productId);
    basket.items[foundItemIndex].quantity++;
    this.setbasket(basket);
  }

  removeItemFromBasket(item:IBasketItem){
    const basket = this.getcurrentBasket();
    if(!basket) return;

    if(basket.items.some((x) => x.productId == item.productId)){
      basket.items =  basket.items.filter((x) => x.productId !== item.productId)
      if(basket.items.length > 0){
        this.setbasket(basket);
      }
      else{
        this.deleteBasket(basket.userName);
      }
    }
  }

  deleteBasket(userName: string) {
      return this.http.delete(this.baseurl + '/Basket/DeleteBasket/' + userName).subscribe({
      next:(response) =>{
        this.basketSource.next(null);
        this.basketTotal.next(null);
        localStorage.removeItem('basket_username');
      }, error: (err)=>{
        console.log('Error Occurred while deletin basket');
        console.log(err);
      }
    })
  }

  decreamentItemQuantity(item:IBasketItem){
     const basket = this.getcurrentBasket();
    if(!basket) return;

    const foundItemIndex = basket.items.findIndex((x) => x.productId === item.productId);
    if(basket.items[foundItemIndex].quantity > 1){
      basket.items[foundItemIndex].quantity--;
    }
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

  private calculateBasketTotal(){
    const basket = this.getcurrentBasket();

    const total = basket?.items.reduce((x, y) => (y.price * y.quantity) + x, 0) ?? 0;
    this.basketTotal.next({ total });
  }

}
