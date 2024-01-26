import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CustomersService } from '../../services/customers/customers.service';
import { faker } from '@faker-js/faker';

@Component({
  selector: 'app-add-customer',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule, CommonModule, HttpClientModule],
  templateUrl: './add-customer.component.html',
  styleUrl: './add-customer.component.scss',
})
export class AddCustomerComponent implements OnInit {
  customerForm: FormGroup;
  constructor(
    private router: Router,
    private customerService: CustomersService
  ) {
    this.customerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  ngOnInit() {}

  populateForm() {
    // or, if desiring a different locale
    this.customerForm.setValue({
      firstName: faker.person.firstName(),
      lastName: faker.person.lastName(),
      email: faker.internet.email(),
    });
  }
  populateFormAndSave() {
    // or, if desiring a different locale
    this.customerForm.setValue({
      firstName: faker.person.firstName(),
      lastName: faker.person.lastName(),
      email: faker.internet.email(),
    });
    this.onSubmit();
  }

  onSubmit() {
    if (this.customerForm.valid) {
      // Call the API to submit the form data
      this.customerService.addCustomer(this.customerForm.value).subscribe({
        next: () => {
          this.router.navigateByUrl('/');
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
