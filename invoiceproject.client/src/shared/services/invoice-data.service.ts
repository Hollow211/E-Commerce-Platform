import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class InvoiceDataService {

  constructor(private http: HttpClient) { }

  getCustomerInvoices(id: number) {
    this.http.get(`${environment.backend}/invoices/overview`)
  }

}
