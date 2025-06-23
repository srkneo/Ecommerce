import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http:HttpClient) {}
  baseUrl = 'http://localhost:8010/';

  getProducts(storeParms:StoreParams) {
    let params = new HttpParams();
    if (storeParms.brandId) {
      params = params.append('brandId', storeParms.brandId);
    }
    if (storeParms.typeId) {
      params = params.append('typeId', storeParms.typeId);
    }

    params = params.append('sort', storeParms.sort);
    params = params.append('pageIndex', storeParms.pageNumber.toString());
    params = params.append('pageSize', storeParms.pageSize.toString());

    return this.http.get<IPagination<IProduct[]>>(this.baseUrl + 'Catalog/GetAllProducts',{params});    
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'Catalog/GetAllBrands');
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'Catalog/GetAllTypes');
  }

}
