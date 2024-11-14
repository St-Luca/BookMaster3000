import { useAuthStore } from "~/features/auth/store"

const authStore = useAuthStore();

export default defineNuxtRouteMiddleware((to, from) => {
  if (!authStore.isAuth) {
    return navigateTo('/auth');
  }
})
