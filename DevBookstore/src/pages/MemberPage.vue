<template>
  <div>
    <!-- 頁首 -->
    <HeaderBar :header-categories="categoryStore.headerCategories"/>
    <div class="container">
      <div class="row mt-4">
        <!-- 側邊欄 -->
        <MemberSideBar :sidebarCategories="memberStore.sidebarCategories" />
        <div class="col-md-10">
          <div v-if="uiStore.loadingMap.user" class="d-flex justify-content-center align-items-center" style="height: 750px;">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </div>
          <router-view v-else />
          
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
import MemberSideBar from '@/components/layout/MemberSideBar.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

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

// watch(
//   () => authStore.isInitialized,
//   (ready) => {
//     if (ready && !authStore.isLoggedIn) {
//       router.push('/')
//       uiStore.openLogin()
//     }
//   },
//   { immediate: true }
// )

</script>