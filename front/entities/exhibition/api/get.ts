import { api } from "~/shared/util"
import { exhibitionsListSample, type Exhibition, type ExhibitionsListResponse } from "../types";

type ApiResponse = {
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
    bookSubjects: Array<string>
  }>
}

export const getExhibitionById = (id:string|number) : Promise<Exhibition|undefined> => {
  return api<ApiResponse>(`/Exhibition/${id}`, {
    method: "GET",
  }).then(res => {
    if (!res._data) return undefined;
    if (res.statusText !== "OK") return undefined;
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
