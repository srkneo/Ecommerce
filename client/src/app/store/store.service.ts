import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/type';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http:HttpClient) {}
  baseUrl = 'http://localhost:8010/';

  getProducts() {
    return this.http.get<IPagination<IProduct[]>>(this.baseUrl + 'Catalog/GetAllProducts');    
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'Catalog/GetAllBrands');
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'Catalog/GetAllTypes');
  }

}
