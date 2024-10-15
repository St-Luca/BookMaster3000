import type { Book } from "~/entities/book";

export interface CirculationRecord {
  key: string;
  book: Book;
  issueDate: Date;
  returnDatePlanned: Date;
  returnDateActual?: Date;
}