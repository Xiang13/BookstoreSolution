import axios from 'axios'
import { useAuthStore } from '@/stores/authStore'
import { useUIStore } from '@/stores/uiStore'
import router from '@/router'

const instance = axios.create({
  baseURL: import.meta.env.VITE_API_URL || '/',
  withCredentials: true
})

instance.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      const authStore = useAuthStore()
      const uiStore = useUIStore()
      console.warn('[Auth] Token 已過期，執行自動登出')
      authStore.logout()
      uiStore.openLogin()
      router.push('/')
    }
    return Promise.reject(error)
  }
)

export default instance
