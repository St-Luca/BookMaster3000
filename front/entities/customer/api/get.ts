import { api } from "~/shared/util";
import type { ApiResultCustomer, Customer } from "../types";
import type { ApiResultCirculationRecord, CirculationRecord } from "~/entities/circulation-record";

const circulationRecordsToDomain = (data:ApiResultCirculationRecord[]) : CirculationRecord[] => 
  data.map((b,i) => ({
    ...b,
    issueFrom: new Date(b.issueFrom),
    issueTo: new Date(b.issueTo),
    returnDate: new Date(b.returnDate),
  }))

export const getCustomer = (id:string) : Promise<Customer|undefined> => {
  return api<ApiResultCustomer>(`/ClientCard/${id}`, {
    method: "GET"
  }).then(res => {
    if (!res._data) return undefined;
    if (res.statusText !== "OK") return undefined;
    if (res._data.id === 0) return undefined;
    return {
      ...res._data,
      issuedBooks: circulationRecordsToDomain(res._data.issuedBooks),
      returnedBooks: circulationRecordsToDomain(res._data.returnedBooks),
    };
  })
}
