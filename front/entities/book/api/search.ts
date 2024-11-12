import { api } from "~/shared/util";
import type { Book, BookListResponse, BookSearchParams } from "../types";

type ApiResponse = {
  itemsCount: number
  page: number;
  pageLimit: number;
  pages: number;
  books: Array<{
    id: number,
    title: string,
    description: string,
    subtitle: string,
    publicationDate: string,
    bookAuthors: Array<string>,
    bookSubjects: Array<string>
  }>
}

export const searchBook = (params: BookSearchParams) : Promise<BookListResponse|false> => {
  const query = Object.entries(params)
    .map(entry => `${entry[0]}=${entry[1]}`)
    .join('&');
  return api<ApiResponse>(`/Books/search?${query}`, {
    method: "GET",
  }).then(res => {
    if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return {
      ...res._data,
      books: res._data.books.map(apiBook => ({
        ...apiBook,
        authors: apiBook.bookAuthors.map((author,i) => ({
          id: i,
          name: author,
        })),
        subjects: apiBook.bookSubjects
      }))
    }
  })
}