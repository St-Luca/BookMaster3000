import type { CirculationRecord } from "~/entities/circulation-record";

export interface Customer {
  id: String;
  name: String;
  address: String;
  zip: String;
  city: String;
  phone: String;
  email: String;
  // circulation: CirculationRecord[];
}