import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket } from '../shared/models/basket';

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
}
