import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Company {
  id: string;
  name: string;
  description: string;
  allowedCurrencies: string[];
  minAmount: number;
  maxAmount: number;
}
export interface CompanyFilters {
  search?: string;
  currency?: string;
}

@Injectable({ providedIn: 'root' })
export class CompanyService {
  private readonly API = 'http://localhost:5213/api/companies';

  constructor(private http: HttpClient) {}

  getCompanies(filters?: CompanyFilters): Observable<Company[]> {
    let params = new HttpParams();

    if (filters?.search)   params = params.set('search',   filters.search);
    if (filters?.currency) params = params.set('currency', filters.currency);

    return this.http.get<Company[]>(this.API, { params });
    }
}