import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, output } from '@angular/core';
import { DataService } from '../../../services/data-service/data.service';
import { animate, Timeline } from 'animejs';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class InvoiceComponent implements AfterViewInit {

  constructor(private data: DataService) {}

  ngAfterViewInit(): void {
    // Animate
    const showTimeLine = new Timeline()
    .add('#invoice',{
      x: ['-2rem','0rem'],
      opacity: [0,1],
      duration: 400,
      ease: 'inOutBack',
    })
    .add('#items tr',{
      x: ['-3rem','0rem'],
      opacity: [0,1],
      duration: 400,
      ease: 'inOut',
    },'-=50');
  }

  @Input() invoice!: any;
  @Input() customer!: any;

  @Output() closeEvent = new EventEmitter<void>();

  onCloseClick() {
    animate('#invoice',{
      x: ['0rem','-5rem'],
      opacity: [1,0],
      duration: 400,
      ease: 'inBack',
      onComplete: () => {
        this.closeEvent.emit();
      }
    })
  }

  payInvoice() {
    this.data.payInvoice(this.invoice.id).subscribe({
      next: (data: any) => {
        if (data == true) {
          this.invoice.isPaid = true;
          (() => {
            animate('#payInfo div',{
              x: ['0rem','3rem'],
              opacity: [1,0],
              duration: 300,
              ease: 'inOut',
            })
            animate('#payButton', {
              y: ['0rem','-1.5rem'],
              opacity: [1,0],
              duration: 300,
              ease: 'inOut',
            });
          })();
        }
      }
    });
  }

}
