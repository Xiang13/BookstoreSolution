// stores/uiStore.js
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue'
import { delay } from '@/utils/delay'

export const useUIStore = defineStore('ui', () => {
  const showLogin = ref(false)
  const isLoading = ref(false)
  const isRegistering = ref(false)

  // 管理 loading 狀態
  const loadingMap = reactive({
    // 登入 / 註冊請求
    auth: false,
    // 書籍請求
    books: false,
    // 購物車請求
    cart: false,
  })

  // 打開登入視窗
  const openLogin = async () => {
      showLogin.value = true
      loadingMap.auth = true
      await delay(2000)
      console.log("載入中", isLoading.value)
      loadingMap.auth = false
  }
  // 關閉登入 / 註冊視窗
  const closeLogin = () => {
    showLogin.value = false
    isRegistering.value = false
  }

  return {    
    loadingMap,
    showLogin,
    isRegistering,
    isLoading,
    delay,
    openLogin,
    closeLogin,
  }
})
