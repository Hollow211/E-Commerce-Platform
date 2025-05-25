import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DataService } from '../../../services/data-service/data.service';
import { ActivatedRoute, ActivatedRouteSnapshot, RouterLink } from '@angular/router';
import { animate } from 'animejs';

@Component({
  selector: 'app-create-invoice',
  templateUrl: './create-invoice.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
})
export class CreateInvoiceComponent implements OnInit {
  constructor(private data: DataService, private route: ActivatedRoute) {}

  AvailableProducts: {id: number, name: string, quantity: number, unitPrice: number, unitsPerPackage: number, packagePrice: number}[] = [];
  Basket: {id: number, name: string, quantity: number, unitPrice: number, isPackage: boolean}[] = [];

  ngOnInit(): void {
    this.data.getAllProducts().subscribe({
      next: (data: any) => {
        this.AvailableProducts = data;
      }
    })
  }

  PickProduct(item: any, isPackage: boolean) {
    var search = this.Basket.find(entry => entry.id == item.id && entry.isPackage == isPackage)
    console.log(search);
    if (search) {
      search.quantity += 1;
    } else {
      this.Basket.push({
        quantity: 1, isPackage: isPackage, name: item.name,
        id: item.id,
        unitPrice: isPackage? item.packagePrice : item.unitPrice
      });
      console.log(this.Basket);
    }
  }

  ClearBasket() {
    this.Basket = [];
  }

  CalculateBasket() {
    let result = 0;
    this.Basket.map((item) => {
      result += (item.unitPrice * item.quantity);
    });
    return result;
  }

  onSubmit() {
    if (this.Basket.length == 0)
      alert("Basket is empty");

    const customerId = this.route.snapshot.paramMap.get('id')!;
    const products = this.Basket.map(item => ({
      id: item.id,
      quantity: item.quantity,
      unitPrice: item.unitPrice,
      isPackage: item.isPackage,
    }));

    const invoiceRequest = {
      customerId: parseInt(customerId),
      TotalAmount: this.CalculateBasket(),
      products,
      InvoiceDate: new Date().toISOString()
    };

    this.data.addInvoice(invoiceRequest).subscribe({
      next: (data: any) => {
        alert(`id of the invoice is ${data.id}`);
      }
    });

  }

  getCurrentId() {
    return this.route.snapshot.paramMap.get('id');
  }
}
