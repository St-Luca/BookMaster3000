import { useAuthStore } from "~/features/auth/store"

const authStore = useAuthStore();

const noAuthAccess = ref([
  "main",
  "exhibitions",
  "browse-books"
] as Array<any>)

export default defineNuxtRouteMiddleware((to, from) => {
  // authStore.isAuth = true;
  if (!authStore.isAuth && !noAuthAccess.value.includes(to.name)) {
    return navigateTo('/auth');
  }
})
