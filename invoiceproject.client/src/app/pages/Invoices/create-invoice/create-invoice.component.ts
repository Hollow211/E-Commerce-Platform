import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DataService } from '../../../services/data-service/data.service';
import { ActivatedRoute, ActivatedRouteSnapshot, RouterLink } from '@angular/router';
import { animate } from 'animejs';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-create-invoice',
  templateUrl: './create-invoice.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink,NgOptimizedImage],
})
export class CreateInvoiceComponent implements OnInit {
  constructor(private data: DataService, private route: ActivatedRoute) {}

  UnitType = UnitType;
  AvailableProducts: Product[] = [];
  Basket: {id: number, name: string, quantity: number, unitId: number, unitType: UnitType, unitPrice: number }[] = [];

  ngOnInit(): void {
    this.data.getAllProducts().subscribe({
      next: (data: any) => {
        this.AvailableProducts = data;
      }
    })
  }

  PickProduct(item: Product, prodUnit: ProductUnit) {
    var search = this.Basket.find(entry => entry.id == item.id && entry.unitId == prodUnit.unitId)

    if (search) {
      search.quantity += 1;
    } else {
      this.Basket.push({
        id: item.id,
        name: item.name,
        quantity: 1,
        unitId: prodUnit.unitId,
        unitType: prodUnit.unit.type,
        unitPrice: prodUnit.unitPrice
      });
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
      alert("Basket is empty"); // Shouldn't happen

    const customerId = this.route.snapshot.paramMap.get('id')!;

    // Get products
    const products: {productId: number, unitId: number, quantity: number}[] = [];

    this.Basket.forEach(item => {
      products.push({
        productId: item.id,
        unitId: item.unitId,
        quantity: item.quantity,
      })
    });

    const invoiceRequest = {
      customerId: parseInt(customerId),
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

export enum UnitType {
  Single = 0,
  Package = 1,
  Bulk = 2
}

export class Unit {
  id!: number;
  type!: UnitType;
}

export class ProductUnit {
  productId!: number;
  unitId!: number;
  unitPrice!: number;
  unit!: Unit;
}

export class Product {
  id!: number;
  name!: string;
  units!: ProductUnit[];
}