import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../services/data-service/data.service';
import { UnitType } from '../../Invoices/create-invoice/create-invoice.component';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
})
export class CreateProductComponent implements OnInit {

  productForm: FormGroup;
  UnitType = UnitType;
  Units!: {id: number, type: UnitType, unit: string}[];
  TakenUnits: {id: number, type: UnitType, unit: string}[] = [];

  constructor(private fb: FormBuilder,private data: DataService) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      unitPriceDTO: this.fb.array([
        this.createUnitForm()
      ])
    });
  }

  ngOnInit(): void {
    this.data.getAllUnits().subscribe({
      next: (data: any) => {
        this.Units = data;
        this.Units.map(entry => {
          entry.unit = UnitType[entry.type];
        })
      }
    })
  }

  createUnitForm(): FormGroup {
    return this.fb.group({
      unitId: [null, Validators.required],
      unitPrice: [null, [Validators.required, Validators.min(0)]]
    });
  }

  get units() {
    return this.productForm.get('unitPriceDTO') as FormArray;
  }

  addUnit() {
    this.units.push(this.createUnitForm());
  }

  removeUnit(index: number) {
    this.units.removeAt(index);
  }

  submitForm() {
    if (this.productForm.valid) {
      const product: CreateProduct = this.productForm.value;
      console.log(product);
      this.data.createProduct(product).subscribe({
        next: (data: any) => {
          alert(data.isSuccess);
        }
      });
    }
  }
}


export interface UnitPriceDTO {
  unitId: number;
  unitPrice: number;
}

export interface CreateProduct {
  name: string;
  unitPriceDTO: UnitPriceDTO[];
}
