import { api } from "~/shared/util";
import type { CreatedExhibition } from "../types";

export const createExhibition = (params:CreatedExhibition) => {
  return api<any>(`/Exhibition`, {
    method: "POST",
    body: {
      ...params
    }
  }).then(res => {
    if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    return true;
  })
}