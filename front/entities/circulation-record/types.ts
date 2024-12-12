export interface CirculationRecord {
  bookId: string|number;
  bookTitle: string;
  issueFrom: Date;
  issueTo: Date;
  returnDate: Date;
  isOverdue: boolean;
}

export interface ApiResultCirculationRecord {
  bookId: string|number;
  bookTitle: string;
  bookSubtitle: string;
  issueFrom: string;
  issueTo: string;
  returnDate: string;
  isOverdue: boolean;
}