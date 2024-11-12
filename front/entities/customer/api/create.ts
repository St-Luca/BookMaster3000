import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";

export const createCustomer = (params:CreatedCustomer) => {
  return api<Customer|string>(`/Clients`, {
    method: "POST",
    body: params
  });
}