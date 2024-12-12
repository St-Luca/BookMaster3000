import { api } from "~/shared/util";

export const returnBook = (customerId:number|string, bookId:number|string) : Promise<boolean> => {
  return api<any>(`/ClientCard/${customerId}/${bookId}/return`, {
    method: "POST"
  }).then(res => {
    if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return true;
  })
}