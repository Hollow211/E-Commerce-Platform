import { Component, ElementRef, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { InvoiceDataService } from '../../../../shared/services/invoice-data/invoice-data.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InvoiceComponent } from '../invoice/invoice.component';
import { CustomerDataService } from '../../../../shared/services/customer-data/customer-data.service';
import { FilterationComponent } from '../filteration/filteration.component';
import { animate, stagger } from 'animejs';
import { startWith } from 'rxjs';
import { DataService } from '../../../services/data-service/data.service';
import { UnitType } from '../create-invoice/create-invoice.component';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  standalone: true,
  imports: [CommonModule, RouterLink],
})
export class OverviewComponent implements OnInit {

  constructor(private data: DataService,private customerData: CustomerDataService,
              private route: ActivatedRoute) {}
  @ViewChild('invoiceContainer', { read: ViewContainerRef }) invoiceContainer!: ViewContainerRef;
  @ViewChild('filterContainer', { read: ViewContainerRef }) filterContainer!: ViewContainerRef;
  filterComponentRef: any;
  
  Invoices: {id: number,totalAmount: number, isPaid: boolean, issueDate: Date, items: {product: {name: string}, unitType: UnitType, quantity: number, unitPrice: number}[]}[] | null = null;
  customer: any;

  // Filteration Variables
  toDate = new Date().toISOString().split('T')[0];
  fromDate = new Date("2017-01-01").toISOString().split('T')[0];

  paidFilter: boolean = false;
  pendingFilter: boolean = false;

  ngOnInit(): void {
    let customerId = Number(this.route.snapshot.paramMap.get('id'));
    this.data.getCustomerInvoices(customerId).subscribe({
      next: (data: any) => {
        this.Invoices = data;
      }

    });
    this.data.getCustomerData(customerId).subscribe({
      next: (data: any) => {
        if (data.isSuccess)
          this.customer = data.customer;
          this.showInvoices_animation();
      }
    });
  }

  onSubmit(index: number) {
    const componentRef = this.invoiceContainer.createComponent(InvoiceComponent);
    componentRef.setInput('invoice',this.ShownInvoices()![index]);
    componentRef.setInput('customer',this.customer);

    componentRef.instance.closeEvent.subscribe({
      next: () => {
        componentRef.destroy();
      }
    })
  }

  getCurrentId() {
    return this.route.snapshot.paramMap.get('id');
  }

  toggleFilter() {
    if (this.filterComponentRef)
    {
      this.filterComponentRef.destroy();
      this.filterComponentRef = null;
      return;
    }
      
    this.filterComponentRef = this.invoiceContainer.createComponent(FilterationComponent);

    // Set Inputs
    this.filterComponentRef.setInput('fromDate',this.fromDate);
    this.filterComponentRef.setInput('toDate',this.toDate);

    this.filterComponentRef.setInput('paidFilter',this.paidFilter);
    this.filterComponentRef.setInput('pendingFilter',this.pendingFilter);

    this.filterComponentRef.instance.DateChangedEvent.subscribe({
      next: (data: any) => {
        this.toDate = data.toDate;
        this.fromDate = data.fromDate;
      }
    })

    this.filterComponentRef.instance.DueChangedEvent.subscribe({
      next: (data: any) => {
        this.paidFilter = data.paidFilter;
        this.pendingFilter = data.pendingFilter;
      }
    })
  }

  ShownInvoices() {
    if (this.Invoices == null)
      return;
    var dateFilter = this.filterByDate(this.fromDate, this.toDate, this.Invoices!);
    var DueFilter = this.filterByDue(this.paidFilter, this.pendingFilter, dateFilter);
    return DueFilter;
  }

  filterByDate(from: string, to: string, invoices: any[]) {
    return invoices.filter(x => {
      const IssueDate = x.issueDate.split('T')[0];
      const isAfter = IssueDate > to;
      const isBefore = IssueDate < from;
      
      return !isAfter && !isBefore;
    })
  }

  filterByDue(PaidFitler: boolean, PendingFilter: boolean, invoices: any[]) {
    return invoices.filter(x => {
      if (PaidFitler)
        return x.isPaid; // return paid ones
      else if (PendingFilter)
        return !x.isPaid;
      else
        return true; // no filter both false
    });
  }
  
  //#region animations
  showInvoices_animation() {
    animate('#invoicesRows tr',{
      x: ['6rem','0rem'],
      opacity: [0,1],
      ease: 'inOut',
      delay: stagger(20),
      duration: stagger(50, {start: 50}),
    });
  }
  //#endregion
}
