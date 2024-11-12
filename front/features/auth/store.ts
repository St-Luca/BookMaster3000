export const useAuthStore = defineStore('auth', {
  state: () => ({
    isAuth: false,
    token: null as null|string,
  }),
  actions: {
    logout() {
      this.isAuth = false;
      this.token = null;
    },
  }
})
