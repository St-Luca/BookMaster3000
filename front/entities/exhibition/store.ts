import type { Book } from "../book";
import { exhibitionAddBook } from "./api/add-book";
import { createExhibition } from "./api/create";
import { getExhibitionsList } from "./api/get-list";
import { exhibitionRemoveBook } from "./api/remove-book";
import type { CreatedExhibition, Exhibition } from "./types";

export const useExhibitionsStore = defineStore('exhibitions', {
  state: () => ({
    list: [] as Exhibition[],
  }),
  actions: {
    async fetch() {
      const list = await getExhibitionsList();
      if (list) this.list = list;
      return list;
    },
    async create(exhibition: CreatedExhibition) {
      const res = await createExhibition(exhibition);
      if (res) this.fetch();
      return res;
    },
    async addBook(book: Book, exhibitionId: string|number) {
      const res = await exhibitionAddBook(book.id, exhibitionId);
      if (res) this.fetch();
      return res;
    },
    async removeBook(book: Book, exhibitionId: string|number) {
      const res = await exhibitionRemoveBook(book.id, exhibitionId);
      if (res) this.fetch();
      return res;
    },
    async toggleBook(book: Book, ex: Exhibition) {
      const hasBook = ex.books.some(b => b.id === book.id);
      console.log(hasBook);
      let res = false;
      if (this.hasBook(ex.id, book.id)) {
        res = await this.removeBook(book, ex.id);
      } else {
        res = await this.addBook(book, ex.id);
      }
      if (res) this.fetch();
    },
    hasBook(ex: string|number, bookId: string|number) {
      const exhibition = this.list.find(e => e.id === ex);
      if (!exhibition) return false;
      return exhibition.books.some(b => b.id === bookId);
    }
  }
})
