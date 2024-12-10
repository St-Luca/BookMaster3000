import { useAuthStore } from "~/features/auth/store"

const authStore = useAuthStore();

const noAuthAccess = ref([
  "main",
  "exhibitions",
] as Array<any>)

export default defineNuxtRouteMiddleware((to, from) => {
  if (!authStore.isAuth && !noAuthAccess.value.includes(to.name)) {
    return navigateTo('/auth');
  }
})
