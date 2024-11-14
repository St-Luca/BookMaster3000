import { api } from "~/shared/util"
import type { AuthForm } from "../types"
import { useAuthStore } from "../store"

export const login = (data:AuthForm) => {
  const authStore = useAuthStore();
  
  return api<null|{token:string}>(`/Auth/login`, {
    method: "POST",
    body: data
  }).then(res => {
    if (!res._data) return false;
    if (res.statusText !== "OK") return false;
    authStore.isAuth = true;
    authStore.token = res._data.token;
    return true;
  })
}