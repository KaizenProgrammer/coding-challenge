import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  AddCustomerRequest,
  AddCustomerResponse,
  Customer,
} from './customers.model';

@Injectable({
  providedIn: 'root',
})
export class CustomersService {
  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>('/api/customers');
  }
  addCustomer(customer: AddCustomerRequest): Observable<AddCustomerResponse> {
    return this.http.post<AddCustomerResponse>('/api/customers', customer);
  }
  updateCustomer(
    id: number,
    customer: AddCustomerRequest
  ): Observable<AddCustomerResponse> {
    return this.http.put<AddCustomerResponse>(`/api/customers/${id}`, customer);
  }
}
