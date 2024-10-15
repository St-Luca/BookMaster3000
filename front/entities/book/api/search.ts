import { api } from "~/shared/util";
import type { BookSearchParams } from "../types";

export const searchCustomer = (params: BookSearchParams) => {
  return api(`/Books/search`, {
    method: "GET",
    body: params,
  })
}