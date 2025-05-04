import { Component, ElementRef, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { InvoiceDataService } from '../../../../shared/services/invoice-data/invoice-data.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { InvoiceComponent } from '../invoice/invoice.component';
import { CustomerDataService } from '../../../../shared/services/customer-data/customer-data.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class OverviewComponent implements OnInit {
  constructor(private invoiceData: InvoiceDataService,private customerData: CustomerDataService,
              private route: ActivatedRoute) {}
  @ViewChild('invoiceContainer', { read: ViewContainerRef }) invoiceContainer!: ViewContainerRef;

  Invoices: any[] | null = null;
  customer: any;

  ngOnInit(): void {
    let customerId = Number(this.route.snapshot.paramMap.get('id'));
    this.invoiceData.getCustomerInvoices(customerId).subscribe({
      next: (data: any) => {
        console.log(data);
        if (data.isSuccess)
          this.Invoices = data.invoices;
      }
    })
    this.customerData.getCustomerData(customerId).subscribe({
      next: (data: any) => {
        console.log(data);
        if (data.isSuccess)
          this.customer = data.customer;
      }
    })
  }

  onSubmit(index: number) {
    const componentRef = this.invoiceContainer.createComponent(InvoiceComponent);
    componentRef.setInput('invoice',this.Invoices![index]);
    componentRef.setInput('customer',this.customer);

    componentRef.instance.closeEvent.subscribe({
      next: () => {
        componentRef.destroy();
      }
    })
  }
}
