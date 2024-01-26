import { Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { AddCustomerComponent } from './customers/add-customer/add-customer.component';

export const routes: Routes = [
  { path: 'api/**', redirectTo: '/' },
  {
    path: '',
    component: CustomersComponent,
    data: { title: 'Dynatron - Customers' },
  },
  {
    path: 'add',
    component: AddCustomerComponent,
    data: { title: 'Dynatron - Add Customers' },
  },
];
