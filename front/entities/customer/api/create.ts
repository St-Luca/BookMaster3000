import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";

export const createCustomer = (params:CreatedCustomer) => {
  return api<Customer|string>(`/ClientCard`, {
    method: "POST",
    body: params
  });
}