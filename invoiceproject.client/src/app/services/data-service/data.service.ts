import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { environment } from '../../../environments/environment.development';
import { CreateProduct } from '../../pages/Products/create-product/create-product.component';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  constructor(private http: HttpClient) { }

  getAllProducts() {
    return this.http.get('/api/product/getProducts');
  }

  getCustomerInvoices(id: number) {
    return this.http.get(`/api/invoice/overview/${id}`);
  }

  getCustomerData(id: number) {
      return this.http.get(`/api/customer/detail/${id}`);
  }

  addInvoice(invoiceRequest: any) {
    return this.http.post('/api/invoice/create',invoiceRequest);
  }

  createCustomer(customer: FormGroup) {
    return this.http.post('/api/customer/create',customer.value);
  }

  payInvoice(id: number) {
    return this.http.post('/api/invoice/pay',{id: id});
  }

  getAllUnits() {
    return this.http.get('/api/unit/get-all');
  }

  createProduct(product: CreateProduct) {
    return this.http.post('/api/product/create-product',product);
  }

}
