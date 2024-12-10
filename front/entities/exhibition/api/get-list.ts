import { api } from "~/shared/util"
import { exhibitionsListSample, type ExhibitionsListResponse } from "../types";

type ApiResponse = {
  itemsCount: number
  page: number;
  pageLimit: number;
  pages: number;
  exhibitions: Array<{
    name: string;
    description: string;
    dateCreated: string;
    books: Array<{
      id: string|number,
      title: string,
      description: string,
      subtitle: string,
      publicationDate: string,
      bookAuthors: Array<string>,
      bookSubjects: Array<string>
    }>;
  }>
}

export const getExhibitionsList = () : Promise<ExhibitionsListResponse|false> => {
  // return new Promise((resolve) => resolve(exhibitionsListSample));
  return api<ApiResponse>(`/Exhibition/?page=${1}`, {
    method: "GET",
  }).then(res => {
    if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return {
      ...res._data,
      exhibitions: res._data.exhibitions.map(ex => ({
        ...ex,
        books: ex.books.map(apiBook => ({
          ...apiBook,
          authors: apiBook.bookAuthors.map((author,i) => ({
            id: i,
            name: author,
          })),
          subjects: apiBook.bookSubjects
        }))
      }))
    }
  })
}
