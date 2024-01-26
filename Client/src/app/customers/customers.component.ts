import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, ElementRef, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CustomersService } from '../services/customers/customers.service';
import { Customer } from '../services/customers/customers.model';
import { DateAgoPipe } from '../pipes/date-ago.pipe';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    DateAgoPipe,
  ],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.scss',
})
export class CustomersComponent implements OnInit {
  customerForm: FormGroup;
  selectedCustomer: number = -1;
  customers: Customer[] = [];
  constructor(
    private customerService: CustomersService,
    private el: ElementRef
  ) {
    this.customerForm = new FormGroup({
      id: new FormControl(-1, Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }
  ngOnInit(): void {
    this.customerService.getCustomers().subscribe((customers) => {
      this.customers = customers;
      this.loadSelectedCustomer();
    });
  }
  select(i: number) {
    this.selectedCustomer = i;
    this.loadCustomerForm();
    this.saveSelectedCustomer();
  }
  saveSelectedCustomer() {
    if (typeof window !== 'undefined') {
      window.sessionStorage.setItem(
        'selectedCustomer',
        JSON.stringify(this.selectedCustomer)
      );
    }
  }

  loadSelectedCustomer() {
    if (typeof window !== 'undefined') {
      const savedCustomer = window.sessionStorage.getItem('selectedCustomer');
      if (savedCustomer) {
        this.selectedCustomer = JSON.parse(savedCustomer);
        this.loadCustomerForm();
      }
    }
  }

  loadCustomerForm() {
    if (this.selectedCustomer < 0) {
      return;
    }
    const customer = this.customers[this.selectedCustomer];
    this.customerForm.setValue({
      id: customer.id,
      firstName: customer.firstName,
      lastName: customer.lastName,
      email: customer.email,
    });
    setTimeout(() => {
      // Autofocus on firstname field
      const invalidControl = this.el.nativeElement.querySelector(
        '[formcontrolname="firstName"]'
      );
      invalidControl.focus();
    });
  }
  onSubmit() {
    if (this.customerForm.valid) {
      // Call the API to submit the form data
      const customer = this.customerForm.value;
      this.customerService.updateCustomer(customer.id, customer).subscribe({
        next: (updatedCustomer) => {
          this.customers[this.selectedCustomer] = updatedCustomer;
          this.selectedCustomer = -1;
        },
        error: (error) => {
          if (error.status === 422) {
            // Check if the error is related to the 'email' field
            if (error.error && error.error.email) {
              // Set the server-side validation error to the 'email' form control
              const emailErrors = { serverError: error.error.email.join(' ') };
              this.customerForm.get('email')?.setErrors(emailErrors);
            }
          }
        },
      });
    }
  }
}
