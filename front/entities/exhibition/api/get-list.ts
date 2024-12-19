import { api } from "~/shared/util"
import { exhibitionsListSample, type Exhibition, type ExhibitionsListResponse } from "../types";

type ApiResponse = {
  itemsCount: number
  page: number;
  pageLimit: number;
  pages: number;
  exhibitions: Array<{
    id: string|number;
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
      bookSubjects: Array<string>,
      bookCovers: Array<string>,
    }>;
  }>
}

export const getExhibitionsList = () : Promise<Array<Exhibition>> => {
  // return new Promise((resolve) => resolve(exhibitionsListSample.exhibitions));
  return api<ApiResponse>(`/Exhibition/?page=-1`, {
    method: "GET",
  }).then(res => {
    if (!res._data) return [];
    if (res.statusText !== "OK") return [];
    const val = {
      ...res._data,
      exhibitions: res._data.exhibitions.map(ex => ({
        ...ex,
        books: ex.books.map(apiBook => ({
          ...apiBook,
          authors: apiBook.bookAuthors.map((author,i) => ({
            id: i,
            name: author,
          })),
          subjects: apiBook.bookSubjects,
          coverImg: apiBook.bookCovers.length ? apiBook.bookCovers[0] : undefined,
        }))
      }))
    }.exhibitions
    console.log(val);
    return val;
  })
}
