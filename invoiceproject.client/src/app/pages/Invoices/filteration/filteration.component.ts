import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filteration',
  templateUrl: './filteration.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class FilterationComponent {
  @Output() DateChangedEvent = new EventEmitter<{fromDate: string, toDate: string}>();
  @Output() DueChangedEvent = new EventEmitter<{paidFilter: boolean, pendingFilter: boolean}>();

  @Input() toDate!: string
  @Input() fromDate!: string;

  @Input() paidFilter: boolean = false;
  @Input() pendingFilter: boolean = false;
  

  fromDateChanged() {
    this.DateChangedEvent.emit({toDate: this.toDate, fromDate: this.fromDate})
  }

  DueChanged() {
    this.DueChangedEvent.emit({paidFilter: this.paidFilter, pendingFilter: this.pendingFilter});
  }

  onPaidClick() {
    this.paidFilter = !this.paidFilter;
    this.DueChanged();
  }

  onPendingClick() {
    this.pendingFilter = !this.pendingFilter;
    this.DueChanged();
  }

}
