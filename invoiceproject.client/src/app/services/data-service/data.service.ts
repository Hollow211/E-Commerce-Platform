import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllProducts() {
    return this.http.get('/api/invoice/getProducts');
  }

  addInvoice(invoiceRequest: any) {
    return this.http.post('/api/invoice/add',invoiceRequest);
  }

  createCustomer(customer: FormGroup) {
    return this.http.post('/api/customer/create',customer.value);
  }

  payInvoice(id: number) {
    return this.http.post('/api/invoice/pay',{id: id});
  }
}
