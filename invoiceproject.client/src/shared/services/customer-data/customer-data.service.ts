import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CustomerDataService {

  constructor(private http: HttpClient) { }

  getCustomerData(id: number) {
      return this.http.get(`${environment.backend}/api/customer/detail/${id}`);
  }
}
