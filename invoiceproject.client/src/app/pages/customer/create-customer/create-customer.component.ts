import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { DataService } from '../../../services/data-service/data.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class CreateCustomerComponent {
  customerForm!: FormGroup;

  constructor(private fb: FormBuilder,private data: DataService) {}

  ngOnInit(): void {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.customerForm.valid) {
      alert('submitted');
      this.data.createCustomer(this.customerForm).subscribe({
        next: (data: any) => alert(`user created id: ${data.id}`)
      });
    } else {
      this.customerForm.markAllAsTouched();
    }
  }
}
