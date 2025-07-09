import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('user', () => {
  const sidebarCategories = ref([
    { key: 'profile', categoryName: '會員資料' },
    { key: 'cart', categoryName: '購物車' },
    { key: 'orders', categoryName: '訂單' },
  ])

  // 當前會員頁籤
  const userCurrentTab = ref('profile') // 預設是會員資料

  return {
    sidebarCategories,
    userCurrentTab
  }
})