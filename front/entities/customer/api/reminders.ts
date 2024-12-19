import { api } from "~/shared/util";
import type { CreatedCustomer, Customer } from "../types";
import type { ApiResultCirculationRecord } from "~/entities/circulation-record";
import { circulationRecordToDomain } from "~/entities/circulation-record/toDomain";
import type { ApiBookReportRecord, BookReportRecord } from "~/entities/report/types";

export const getRemindersReport = () : Promise<BookReportRecord[]> => {
  return api<ApiBookReportRecord[]>(`/ClientCard/reminders`, {
    method: "GET",
  }).then(res => res._data?.map(item => ({
    ...item,
    ...circulationRecordToDomain(item)
  })) ?? [])
}