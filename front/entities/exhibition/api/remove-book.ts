import { api } from "~/shared/util";

export const exhibitionRemoveBook = (bookId: string|number, exhibitionId: string|number) => {
  return api<any>(`/Exhibition/${bookId}/${exhibitionId}/removeBook`, {
    method: "POST",
  }).then(res => {
    // if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return true;
  })
}