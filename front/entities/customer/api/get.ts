import { api } from "~/shared/util";
import type { Customer } from "../types";

export const getCustomer = (id:string) : Promise<Customer|undefined> => {
  return api<Customer>(`/ClientCard/${id}`, {
    method: "GET"
  }).then(res => {
    if (!res._data) return undefined;
    if (res.statusText !== "OK") return undefined;
    if (res._data.id === 0) return undefined;
    return res._data;
  })
}
