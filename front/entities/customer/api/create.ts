import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";

export const createCustomer = (params:CreatedCustomer) : Promise<Customer> => {
  return api(`/Clients/`, {
    method: "POST",
    body: params
  });
}