import { defineStore } from 'pinia'
import { ref, onMounted, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useUIStore } from '@/stores/uiStore'
import { useAuthStore } from '@/stores/authStore'
import { useOrderStore } from '@/stores/orderStore'
import { useCartStore } from '@/stores/cartStore'
import { delay } from '@/utils/delay'

export const useMemberStore = defineStore('member', () => {
  const router = useRouter()
  const route = useRoute()
  const uiStore = useUIStore()
  const authStore = useAuthStore()
  const orderStore = useOrderStore()
  const cartStore = useCartStore()

  const userCurrentTab  = ref('')
  watch(() => route.path, () => {
    loadDataByTab()
  })
const loadDataByTab = async () => {
  const tab = route.path.split('/').pop() || 'profile'
  userCurrentTab.value = tab
  uiStore.loadingMap.user = true
  try {
    await delay(500) // 模擬載入
    switch (tab) {
      case 'profile':
        await authStore.checkAuthStatus()
        break
      case 'orders':
        await orderStore.fetchOrderList()
        break
      case 'cart':
        await cartStore.fetchCartItems()
        break
    }
  } finally {
    uiStore.loadingMap.user = false
  }
}
  const sidebarCategories = ref([
    { key: 'profile', categoryName: '會員資料' },
    { key: 'orders', categoryName: '訂單' },
    { key: 'cart', categoryName: '購物車' },
  ])

  return {
    sidebarCategories,
    userCurrentTab,
  }
})