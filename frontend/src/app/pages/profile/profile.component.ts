import { Component, OnInit } from '@angular/core';
import { AuthService,User } from 'src/app/shared/services/auth.service';
import { PaymentService,Payment } from 'src/app/shared/services/payment.service';
import { Router } from '@angular/router';

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

  constructor(
    private authService: AuthService,
    private paymentService: PaymentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.user = user;
    });
    this.loadPayments();
  }

  loadPayments(): void {
    this.loading = true;
    this.paymentService.getUserPayments().subscribe({
      next: (data) => {
        this.payments = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  logout(): void {
    this.authService.logout();
  }

  onPaymentCreated(payment: Payment): void {
    this.payments.unshift(payment);  // add to top of list instantly
  }


}