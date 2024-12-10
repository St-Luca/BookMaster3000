import type { ApiResultCirculationRecord, CirculationRecord } from "~/entities/circulation-record";

export interface Customer {
  id: string|number;
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
  issuedBooks: CirculationRecord[];
  returnedBooks: CirculationRecord[];
}

export interface ApiResultCustomer {
  id: string|number;
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
  issuedBooks: ApiResultCirculationRecord[];
  returnedBooks: ApiResultCirculationRecord[];
}

export interface CustomerSearchParams {
  id?: string;
  name?: string;
  page: number;
}

export interface CreatedCustomer {
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
}

export interface EditedCustomer {
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
}