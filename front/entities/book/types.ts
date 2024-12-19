import type { Author } from "~/entities/author";

export interface Book {
  id: string|number;
  title: string;
  subtitle: string;
  description: string;
  firstPublishDate?: string;
  coverImg?: string;
  subjects?: string[];
  // covers: string[];
  authors: Author[];
}

export interface BookSearchParams {
  title?: string;
  author?: string;
  subject?: string;
  page: Number;
}

export interface BookListResponse {
  itemsCount: number
  page: number;
  pageLimit: number;
  pages: number;
  books: Array<Book>
}