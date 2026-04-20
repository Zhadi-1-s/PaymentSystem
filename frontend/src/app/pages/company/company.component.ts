import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Company,CompanyService,CompanyFilters } from 'src/app/shared/services/company.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit {
  companies: Company[] = [];
  loading = false;
  filters: CompanyFilters = {};

  currencies = ['USD', 'EUR', 'RUB'];

  constructor(
    private companyService: CompanyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loading = true;
    this.loadCompanies();
  }

  loadCompanies(): void {
    this.loading = true;
    this.companyService.getCompanies(this.filters).subscribe({
      next: (data) => {
        this.companies = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }
   applyFilters(): void {
    this.loadCompanies();
  }

  resetFilters(): void {
    this.filters = {};
    this.loadCompanies();
  }
  selectCompany(company: Company): void {
    // Save selected company to sessionStorage, then go to profile to make payment
    sessionStorage.setItem('selectedCompany', JSON.stringify(company));
    this.router.navigate(['/profile']);
  }
}