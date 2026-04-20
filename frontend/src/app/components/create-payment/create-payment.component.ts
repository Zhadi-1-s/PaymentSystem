import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Payment,PaymentService } from 'src/app/shared/services/payment.service';
import { AuthService,User } from 'src/app/shared/services/auth.service';
import { Company } from 'src/app/shared/services/company.service';

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

  selectedCompany:Company | null = null;

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

    const raw = sessionStorage.getItem('selectedCompany');
    if (raw) {
    this.selectedCompany = JSON.parse(raw);
      
      this.currencies = this.selectedCompany!.allowedCurrencies;
      this.form.patchValue({ currency: this.currencies[0] });
    }
    
    this.authService.currentUser$.subscribe((user: User | null) => {
      if (user) {
        this.form.patchValue({
          account:user.name,
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

    const payload = {
      ...this.form.value,
      companyid:this.selectedCompany?.id
    }

    this.paymentService.createPayment(payload).subscribe({
      next: (payment) => {
        console.log('here is the payment',payment)
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

  // Close modal
  onBackdropClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-backdrop')) {
      this.close();
    }
  }
}