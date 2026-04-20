import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { StatsResponse,PaymentService } from 'src/app/shared/services/payment.service';

@Component({
  selector: 'app-stats-modal',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  @Output() close = new EventEmitter<void>();
  stats$!: Observable<StatsResponse>;

  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.stats$ = this.paymentService.getStats();
  }

  onClose() {
    this.close.emit();
  }
}