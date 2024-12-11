import { api } from "~/shared/util";

export const exhibitionAddBook = (bookId: string|number, exhibitionId: string|number) => {
  return api<any>(`/Exhibition/${bookId}/${exhibitionId}/addBook`, {
    method: "POST",
  }).then(res => {
    // if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return true;
  })
}