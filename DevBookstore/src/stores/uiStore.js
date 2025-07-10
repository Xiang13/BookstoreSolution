// 管理 UI 狀態的 Pinia store
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue'
import { delay } from '@/utils/delay'

export const useUIStore = defineStore('ui', () => {
  // 控制是否顯示登入 Modal
  const showLogin = ref(false)
  // 控制載入動畫
  const isLoading = ref(false)  
  // 管理 loading 狀態
  const loadingMap = reactive({
    // 登入 / 註冊
    auth: false,
    // 書籍
    books: false,
    // 購物車
    user: false,
  })

  // 打開登入視窗
  const openLogin = async () => {
      showLogin.value = true
      loadingMap.auth = true
      await delay(500)
      loadingMap.auth = false
  } 

  // 關閉登入 / 註冊視窗
  const closeLogin = () => {
    showLogin.value = false
  }

  return {    
    loadingMap,
    showLogin,
    isLoading,
    delay,
    openLogin,
    closeLogin,
  }
})
