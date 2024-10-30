import { api } from "~/shared/util";
import type { Customer } from "../types";

export const getCustomer = (id:string) : Promise<Customer|undefined> => {
  return api<Customer>(`/Clients/${id}`, {
    method: "GET"
  }).then(res => {
    if (!res._data) return undefined;
    if (res.statusText !== "OK") return undefined;
    return res._data;
  })
}