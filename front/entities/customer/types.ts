import type { CirculationRecord } from "~/entities/circulation-record";

export interface Customer {
  id: String;
  name: String;
  phone: String;
  email: String;
  city?: String;
  address?: String;
  zip?: String;
  // circulation: CirculationRecord[];
}

export interface CreatedCustomer {
  name: String;
  phone: String;
  email: String;
  city?: String;
  address?: String;
  zip?: String;
}

export interface EditedCustomer {
  name: String;
  phone: String;
  email: String;
  city?: String;
  address?: String;
  zip?: String;
}