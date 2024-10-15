import { api } from "~/shared/util";
import type { Customer } from "../types";

export const searchCustomer = (query:string) : Promise<Array<Customer>> => {
  return api(`/Clients/search`, {
    method: "GET",
    body: {
      name: query
    },
  })
}