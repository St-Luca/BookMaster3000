import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";

export const editCustomer = (id:string, params:CreatedCustomer) => {
  return api<Customer|string>(`/ClientCard/${id}`, {
    method: "PUT",
    body: params,
  })
}