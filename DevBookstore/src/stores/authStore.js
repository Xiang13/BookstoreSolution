import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const isLoggedIn = ref(false)

  // 登入
  const login = (name) => {
    isLoggedIn.value = true
  }

  // 登出
  const logout = () => {
    isLoggedIn.value = false
    localStorage.removeItem('token')
    localStorage.removeItem('displayName')
    localStorage.removeItem('rememberedEmail')
  }

  // 判斷登入狀態
  const checkLoginStatus = () => {
    const token = localStorage.getItem('token')
    if (token) {
      isLoggedIn.value = true
    }
  }

  return {
    isLoggedIn,
    login,
    logout,
    checkLoginStatus,
  }
})
