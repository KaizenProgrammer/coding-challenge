export interface AddCustomerRequest {
  firstName: string;
  lastName: string;
  email: string;
}

export interface AddCustomerResponse extends Customer {}

export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  createdDate: Date;
  updatedDate: Date;
}
