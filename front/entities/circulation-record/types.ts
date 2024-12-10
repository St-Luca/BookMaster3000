import type { Book } from "~/entities/book";

export interface CirculationRecord {
  key: string|number;
  bookTitle: string;
  issueFrom: Date;
  issueTo: Date;
  returnDate: Date;
  isOverdue: boolean;
}

export interface ApiResultCirculationRecord {
  bookTitle: string;
  bookSubtitle: string;
  issueFrom: string;
  issueTo: string;
  returnDate: string;
  isOverdue: boolean;
}