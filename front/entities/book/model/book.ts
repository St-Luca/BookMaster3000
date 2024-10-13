import type { Author } from "~/entities/author";

export interface Book {
  key: string;
  title: string;
  subtitle: string;
  description: string;
  firstPublishDate?: string;
  coverImg?: string;
  subjects?: string[];
  authors: Author[];
}