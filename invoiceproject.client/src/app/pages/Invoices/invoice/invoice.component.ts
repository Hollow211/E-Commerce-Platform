import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, output } from '@angular/core';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class InvoiceComponent {
  @Input() invoice!: any;
  @Input() customer!: any;

  @Output() closeEvent = new EventEmitter<void>();

  onCloseClick() {
    this.closeEvent.emit();
  }

}
