<template>
  <div>
    <!-- 頁首 -->
    <HeaderBar :header-categories="categoryStore.headerCategories"/>
    <div class="container">
      <div class="row mt-4">
        <!-- 側邊欄 -->
        <SideBar
          v-model="memberStore.userCurrentTab"
          :sidebarCategories="memberStore.sidebarCategories"
          keyField="key"
          labelField="categoryName"
          :onItemSelected="handleMemberTabChange"
        />
        <div class="col-md-10">
          <div v-if="uiStore.loadingMap.user" class="d-flex justify-content-center align-items-center" style="height: 750px;">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </div>
          <component :is="currentTabComponent" />
          
        </div>
      </div>
    </div>

    <!-- 頁尾 -->
    <FooterBar :footer-categories="categoryStore.footerCategories" />

    <!-- 登入 / 登出視窗 -->
    <UserAuthModal />
    
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { delay } from '@/utils/delay'

// layout
import HeaderBar from '@/components/layout/HeaderBar.vue'
import SideBar from '@/components/layout/SideBar.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

// member
import Member from '@/components/member/Member.vue'
import Orders from '@/components/member/Orders.vue'
import Cart from '@/components/member/Cart.vue'

// models
import UserAuthModal from '@/components/modals/UserAuthModal.vue'

// stores
import { useUIStore } from '@/stores/uiStore'
import { useCategoryStore } from '@/stores/categoryStore'
import { useMemberStore } from '@/stores/MemberStore'

// store 實例
const uiStore = useUIStore()
const categoryStore = useCategoryStore()
const memberStore = useMemberStore()

const handleMemberTabChange = (key) => {
  console.log('[UserPage] 切換到:', key)
  // 這裡可選擇載入資料，如 fetchOrders() 等
}

const currentTabComponent = computed(() => {
  switch (memberStore.userCurrentTab) {
    case 'member': return Member
    case 'orders': return Orders
    case 'cart': return Cart
  }
})



// 掛載時執行
onMounted(async () => {
  uiStore.loadingMap.user = true
  try {
      // 模擬延遲 0.5 秒
      await delay(500)
  } catch (err) {
      console.log("selectBook 錯誤", err)
  } finally {
      // 隱藏 loading
      uiStore.loadingMap.user = false
  }

  
})
</script>