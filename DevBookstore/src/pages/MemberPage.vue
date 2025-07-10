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
          :onItemSelected="memberStore.handleMemberTabChange"
        />
        <div class="col-md-10">
          <div v-if="uiStore.loadingMap.user" class="d-flex justify-content-center align-items-center" style="height: 750px;">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </div>
          <Profile v-if="memberStore.userCurrentTab === 'profile'" v-model:profile="authStore.userProfile" />
          <OrdersList v-else-if="memberStore.userCurrentTab === 'orders'"  />
          <CartView v-else-if="memberStore.userCurrentTab === 'cart'"  />
          
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
import { watch, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { delay } from '@/utils/delay'

// layout
import HeaderBar from '@/components/layout/HeaderBar.vue'
import SideBar from '@/components/layout/SideBar.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

// member
import Profile from '@/components/member/Profile.vue'
import OrdersList from '@/components/member/Orders.vue'
import CartView from '@/components/member/Cart.vue'

// models
import UserAuthModal from '@/components/modals/UserAuthModal.vue'

// stores
import { useUIStore } from '@/stores/uiStore'
import { useAuthStore } from '@/stores/authStore'
import { useCategoryStore } from '@/stores/categoryStore'
import { useMemberStore } from '@/stores/MemberStore'

// store 實例
const uiStore = useUIStore()
const categoryStore = useCategoryStore()
const authStore = useAuthStore()
const memberStore = useMemberStore()

const router = useRouter()

watch(
  () => authStore.isInitialized,
  (ready) => {
    if (ready && !authStore.isLoggedIn) {
      router.push('/')
      uiStore.openLogin()
    }
  },
  { immediate: true }
)

</script>