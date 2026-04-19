import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Payment,PaymentService } from 'src/app/shared/services/payment.service';
import { AuthService,User } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-create-payment-modal',
  templateUrl: './create-payment.component.html',
  styleUrls: ['./create-payment.component.scss']
})
export class CreatePaymentModalComponent implements OnInit {
  @Input()  isOpen = false;
  @Output() closed  = new EventEmitter<void>();
  @Output() created = new EventEmitter<Payment>();

  form: FormGroup;
  loading = false;
  errorMessage = '';

  currencies = ['USD', 'EUR', 'RUB'];

  constructor(
    private fb: FormBuilder,
    private paymentService: PaymentService,
    private authService: AuthService
  ) {
    this.form = this.fb.group({
      walletNumber: ['', Validators.required],
      account:      ['', Validators.required],
      email:        ['', [Validators.required, Validators.email]],
      phone:        [''],
      amount:       [null, [Validators.required, Validators.min(0.01)]],
      currency:     ['USD', Validators.required],
      comment:      ['']
    });
  }

  ngOnInit(): void {
    // Pre-fill email and phone from logged-in user
    this.authService.currentUser$.subscribe((user: User | null) => {
      if (user) {
        this.form.patchValue({
          email: user.email,
          phone: user.phone || ''
        });
      }
    });
  }

  get walletNumber() { return this.form.get('walletNumber')!; }
  get account()      { return this.form.get('account')!; }
  get email()        { return this.form.get('email')!; }
  get amount()       { return this.form.get('amount')!; }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.loading = true;
    this.errorMessage = '';

    this.paymentService.createPayment(this.form.value).subscribe({
      next: (payment) => {
        this.created.emit(payment);
        this.close();
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to create payment. Try again.';
        this.loading = false;
      }
    });
  }

  close(): void {
    this.form.reset({ currency: 'USD' });
    this.errorMessage = '';
    this.closed.emit();
  }

  // Close modal when clicking backdrop
  onBackdropClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-backdrop')) {
      this.close();
    }
  }
}