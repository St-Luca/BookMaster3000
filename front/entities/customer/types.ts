import type { CirculationRecord } from "~/entities/circulation-record";

export interface Customer {
  id: string;
  name: string;
  phone: string;
  email: string;
  city?: string;
  address?: string;
  zip?: string;
  // circulation: CirculationRecord[];
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