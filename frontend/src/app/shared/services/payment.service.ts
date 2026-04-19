import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CreatePaymentRequest {
  walletNumber: string;
  account: string;
  email: string;
  phone?: string;
  amount: number;
  currency: string;
  comment?: string;
}

export interface Payment {
  id: string;
  walletNumber: string;
  account: string;
  email: string;
  phone?: string;
  amount: number;
  currency: string;
  status: string;
  comment?: string;
  createdAt: string;
}

export interface StatsResponse {
  totalAmount: number;
  totalTransactions: number;
  dailyBreakdown: DailyStats[];
}

export interface DailyStats {
  date: string;
  count: number;
  total: number;
}

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private readonly API = 'http://localhost:5213/api/payments';

  constructor(private http: HttpClient) {}

  createPayment(data: CreatePaymentRequest): Observable<Payment> {
    return this.http.post<Payment>(this.API, data);
  }

  getUserPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.API}/user`);
  }

  getStats(): Observable<StatsResponse> {
    return this.http.get<StatsResponse>(`${this.API}/stats`);
  }
}