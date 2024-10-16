import { api } from "~/shared/util";
import type { Customer } from "../types";

export const searchCustomers = (name:string) : Promise<Array<Customer>> => {
  return api<Array<Customer>>(`/Clients/search?name=${name}`, {
    method: "GET"
  }).then(res => {
    if (!res._data) return [];
    if (res.statusText !== "OK") return [];
    return res._data;
  })
}