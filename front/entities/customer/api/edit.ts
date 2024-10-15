import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";

export const createCustomer = (id:string, params:CreatedCustomer) : Promise<Customer> => {
  return api(`/Clients/${id}`, {
    method: "PUT",
    body: params,
  })
}