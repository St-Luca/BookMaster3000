import { circulationRecordToDomain } from "~/entities/circulation-record/toDomain";
import type { ApiBookReportRecord, BookReportRecord } from "~/entities/report/types";
import { api } from "~/shared/util";

export const getBookCirculationHistory = (id: string|number) : Promise<BookReportRecord[]> => {
  return api<ApiBookReportRecord[]>(`/ClientCard/history/${id}`, {
    method: "GET",
  }).then(res => res._data?.map(item => ({
    ...item,
    ...circulationRecordToDomain(item)
  })) ?? [])
}