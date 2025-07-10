import { defineStore } from 'pinia'
import { ref, onMounted, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useUIStore } from '@/stores/uiStore'
import { useAuthStore } from '@/stores/authStore'
import { delay } from '@/utils/delay'

export const useMemberStore = defineStore('member', () => {
  const router = useRouter()
  const route = useRoute()
  const uiStore = useUIStore()
  const authStore = useAuthStore()
  const userCurrentTab  = ref('')
  

  const sidebarCategories = ref([
    { key: 'profile', categoryName: '會員資料' },
    { key: 'orders', categoryName: '訂單' },
    { key: 'cart', categoryName: '購物車' },
  ])

  // 根據路由載入對應資料
  const loadDataByTab = async () => {
    const tab = route.path.split('/').pop() || 'profile'
    userCurrentTab.value = tab
    uiStore.loadingMap.user = true
    try {
      await delay(500) // 模擬延遲

      switch (tab) {
        case 'profile':
          console.log('載入會員資料...')
          if (!authStore.userProfile.userId) {
            console.log('尚未取得會員資料，開始請求')
            await authStore.checkAuthStatus()
          } else {
            console.log('已有會員資料，不重複取得')
          }
          break
        case 'orders':
          console.log('載入訂單資料...')
          await fetchOrders()
          break
        case 'cart':
          console.log('載入購物車資料...')
          await fetchCart()
          break

        default:
          null
      }
    } finally {
      uiStore.loadingMap.user = false
    }
  }

  // 掛載時執行
  onMounted(async () => {
     // 未登入時不載入會員資料
    if (!authStore.isLoggedIn && userCurrentTab.value === 'profile') {
      console.warn('尚未登入，略過會員資料載入')
      return
    }
    uiStore.loadingMap.user = true
    try {
        // 模擬延遲 0.5 秒
        await delay(500)
        loadDataByTab()
    } catch (err) {
        console.log("selectBook 錯誤", err)
    } finally {
        // 隱藏 loading
        uiStore.loadingMap.user = false
    }

  })

  watch(() => route.path, () => {
    loadDataByTab()
  })

  // 根據選擇的 key (categoryName) 切換書籍分類
  const handleMemberTabChange = async (key) => {
    // uiStore.loadingMap.user = true
     try {
        router.push(`/member/${key}`)
      } catch (err) {
        console.error('[UserPage] 切換 tab 發生錯誤', err)
      } finally {
    }
  }

  // 取得訂單資訊
  const fetchOrders =  async () => {

  }

  // 取得購物車資訊
  const fetchCart =  async () => {

  }
  return {
    handleMemberTabChange,
    loadDataByTab,
    sidebarCategories,
    userCurrentTab,
  }
})