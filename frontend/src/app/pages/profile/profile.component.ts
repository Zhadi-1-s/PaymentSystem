import { Component, OnInit } from '@angular/core';
import { AuthService,User } from 'src/app/shared/services/auth.service';
import { PaymentService,Payment, PaymentFilters } from 'src/app/shared/services/payment.service';
import { Router } from '@angular/router';
import { Company } from 'src/app/shared/services/company.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: User | null = null;
  payments: Payment[] = [];
  loading = false;

  isModalOpen = false;

  filters : PaymentFilters = {};

  selectedCompany:Company | null = null;

  constructor(
    private authService: AuthService,
    private paymentService: PaymentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.user = user;
    });
    const raw = sessionStorage.getItem('selectedCompany');
    if (raw) this.selectedCompany = JSON.parse(raw);

    this.loadPayments();
  }

  loadPayments(): void {
    this.loading = true;
    this.paymentService.getUserPayments(this.filters).subscribe({
      next: (data) => {
        this.payments = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  applyFilters(filters: PaymentFilters): void {
    this.filters = filters;
    this.loadPayments();
  }

  resetFilters(): void {
    this.filters = {};
    this.loadPayments();
  }

  logout(): void {
    this.authService.logout();
  }

  onPaymentCreated(payment: Payment): void {
    this.payments.unshift(payment);  // add to top of list instantly
  }

  changeCompany(): void {
    this.router.navigate(['/companies']);
  }


}