import type { ApiResultCirculationRecord, CirculationRecord } from "../circulation-record";

export type BookReportRecord = CirculationRecord & {
  clientId: string;
  clientName: string;
}

export type ApiBookReportRecord = ApiResultCirculationRecord & {
  clientId: string;
  clientName: string;
}
