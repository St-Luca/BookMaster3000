import { api } from "~/shared/util";

type ApiResponse = {}

export const extendBook = (customerId:number|string, bookId:number|string) : Promise<boolean> => {
  return api<any>(`/ClientCard/${customerId}/${bookId}`, {
    method: "PUT"
  }).then(res => {
    console.log(res);
    // if (!res._data) return false;
    // if (res.statusText !== "OK") return false;
  })
}