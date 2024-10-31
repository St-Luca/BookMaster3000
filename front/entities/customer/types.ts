import type { CirculationRecord } from "~/entities/circulation-record";

export interface Customer {
  id: string|number;
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
  hasBooks: CirculationRecord[];
  circulationHistory: CirculationRecord[];
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