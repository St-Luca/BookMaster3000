import { api } from "~/shared/util";
import type { Customer } from "../types";

export const searchCustomers = (name:string, page:number) : Promise<Array<Customer>> => {
  return api<Array<Customer>>(`/ClientCard/search?name=${name}&page=${page}`, {
    method: "GET"
  }).then(res => {
    if (!res._data) return [];
    if (res.statusText !== "OK") return [];
    return res._data;
  })
}