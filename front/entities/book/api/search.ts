import { api } from "~/shared/util";
import type { Book, BookSearchParams } from "../types";

type ApiResponse = Array<{
  id: number,
  title: string,
  description: string,
  subtitle: string,
  publicationDate: string,
  bookAuthors: Array<string>,
  bookSubjects: Array<string>
}>

export const searchBook = (params: BookSearchParams) : Promise<Array<Book>> => {
  const query = Object.entries(params)
    .map(entry => `${entry[0]}=${entry[1]}`)
    .join('&');
  return api<ApiResponse>(`/Books/search?${query}`, {
    method: "GET",
  }).then(res => {
    if (!res._data) return [];
    if (res.statusText !== "OK") return [];
    return res._data.map(apiBook => ({
      ...apiBook,
      authors: apiBook.bookAuthors.map((author,i) => ({
        id: i,
        name: author,
      })),
      subjects: apiBook.bookSubjects
    }))
  })
}