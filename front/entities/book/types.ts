import type { Author } from "~/entities/author";

export interface Book {
  id: number;
  title: string;
  subtitle: string;
  description: string;
  firstPublishDate?: string;
  coverImg?: string;
  subjects?: string[];
  authors: Author[];
}

export interface BookSearchParams {
  title?: string;
  author?: string;
  subject?: string;
}