// 管理使用者登入狀態的 Pinia store
import { defineStore } from 'pinia'
import { parseJwt } from '@/utils/parseJwt'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const isLoggedIn = ref(false)
  const userId = ref(null)
  const displayName = ref(null)
  const roles = ref([])

  // 登入狀態
  const setUserFromToken = (token) => {
    const payload = parseJwt(token)
    userId.value = payload.userId
    displayName.value = payload.displayName
    roles.value = Array.isArray(payload.role) ? payload.role : [payload.role]
    isLoggedIn.value = true
  }

  const login = (token) => {
    localStorage.setItem('token', token)
    setUserFromToken(token)
  }

  // 登出並清除 token、displayName
  const logout = () => {
    isLoggedIn.value = false
    userId.value = null
    displayName.value = null
    roles.value = []
    localStorage.removeItem('token')
    localStorage.removeItem('remberName')
  }

  // 判斷登入狀態
  const checkLoginStatus = () => {
    const token = localStorage.getItem('token')
    if (token) {
      setUserFromToken(token)
    }
  }
  const checkTokenExpiration = () => {
    const token = localStorage.getItem('token')
    if (!token) return false

    const payload = parseJwt(token)
    const now = Math.floor(Date.now() / 1000) // 目前時間（秒）

    if (payload.exp && payload.exp < now) {
      logout()
      alert('登入已過期，請重新登入')
      return false
    }
    return true
  }

  const hasRole = (role) => {
    return roles.value.includes(role)
  }

  return {
    isLoggedIn,
    userId,
    displayName,
    roles,
    login,
    logout,
    checkLoginStatus,
    checkTokenExpiration,
    hasRole,
  }
})
