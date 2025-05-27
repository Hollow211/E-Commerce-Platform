import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../services/data-service/data.service';
import { UnitType } from '../../Invoices/create-invoice/create-invoice.component';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class CreateProductComponent implements OnInit {

  productForm: FormGroup;
  UnitType = UnitType;
  Units!: {id: number, type: UnitType, unit: string}[];

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

  get units() {
    return this.productForm.get('unitPriceDTO') as FormArray;
  }

  createUnitForm(): FormGroup {
    return this.fb.group({
      unitId: [null, Validators.required],
      unitPrice: [null, [Validators.required, Validators.min(0)]]
    });
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
      // Call your backend API here
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
