import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';

export interface CreatePaymentRequest {
  walletNumber: string;
  account: string;
  email: string;
  phone?: string;
  amount: number;
  currency: string;
  comment?: string;
  companyId?:string
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
  companyName?:string;
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

export interface PaymentFilters {
  status?: string;
  currency?: string;
  dateFrom?: string;
  dateTo?: string;
}

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private readonly API = 'http://localhost:5213/api/payments';

  constructor(private http: HttpClient) {}

  createPayment(data: CreatePaymentRequest): Observable<Payment> {
    return this.http.post<Payment>(this.API, data);
  }

  getStats(): Observable<StatsResponse> {
    return this.http.get<StatsResponse>(`${this.API}/stats`);
  }

  getUserPayments(filters?: PaymentFilters): Observable<Payment[]> {
    let params = new HttpParams();

    if (filters?.status)   params = params.set('status',   filters.status);
    if (filters?.currency) params = params.set('currency', filters.currency);
    if (filters?.dateFrom) params = params.set('dateFrom', filters.dateFrom);
    if (filters?.dateTo)   params = params.set('dateTo',   filters.dateTo);

    return this.http.get<Payment[]>(`${this.API}/user`, { params });
  }

}