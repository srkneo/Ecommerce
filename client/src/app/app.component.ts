import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'eShopping';

  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // Example of making an HTTP request
    this.http.get<IPagination<IProduct[]>>('http://localhost:8010/Catalog/GetAllProducts').subscribe({

      next: response =>{
        this.products = response.data;
        console.log(response)

      } ,
      error: error => console.error('There was an error!', error),
      complete: () => console.log('HTTP request completed')

    })
  }
}
