import type { ApiResultCirculationRecord, CirculationRecord } from "./types";

export const circulationRecordsToDomain = (data:ApiResultCirculationRecord[]) : CirculationRecord[] => 
  data.map((b,i) => circulationRecordToDomain(b))

export const circulationRecordToDomain = (data:ApiResultCirculationRecord) : CirculationRecord => ({
  ...data,
  issueFrom: new Date(data.issueFrom),
  issueTo: new Date(data.issueTo),
  returnDate: new Date(data.returnDate),
})