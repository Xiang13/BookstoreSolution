<template>
  <div>
    <!-- 頁首 -->
    <HeaderBar :header-categories="categoryStore.headerCategories"/>
    <div class="container">
      <div class="row mt-4">
        <!-- 側邊欄 -->
         <SideBar
            
            :sidebarCategories="categoryStore.sidebarCategories"
            keyField="categoryId"
            labelField="categoryName"
            :onItemSelected="bookStore.handleBookCategoryChange"
          />
        <div class="col-md-10">
          <div v-if="uiStore.loadingMap.books" class="d-flex justify-content-center align-items-center" style="height: 750px;">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </div>
          <div v-else>
            <!-- 書籍輪播 -->             
            <BooksCarousel
              v-if="bookStore.isCarouselView"
              :categories="categoryStore.carouselCategories"
              :carouselBooksMap="categoryStore.carouselBooks"
            />
            <BooksList v-else-if="bookStore.isBookListView" :books="bookStore.filteredBooks" />
            <!-- 書籍詳細內容 -->
            <BookDetail  v-else-if="bookStore.selectedBook" :book="bookStore.selectedBook" />
          </div>
          
        </div>
      </div>
    </div>

    <!-- 頁尾 -->
    <FooterBar 
      :footer-categories="categoryStore.footerCategories" />
    <!-- 登入 / 登出視窗 -->
     <UserAuthModal />
  </div>
</template>

<script setup>
import { onMounted, onUnmounted } from 'vue'
import { delay } from '@/utils/delay'

// layout
import HeaderBar from '@/components/layout/HeaderBar.vue'
import SideBar from '@/components/layout/SideBar.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

// books
import BooksCarousel from '@/components/books/BooksCarousel.vue'
import BooksList from '@/components/books/BooksList.vue'
import BookDetail from '@/components/books/BookDetail.vue'

// models
import UserAuthModal from '@/components/modals/UserAuthModal.vue'

// stores
import { useUIStore } from '@/stores/uiStore'
import { useBookStore } from '@/stores/bookStore'
import { useCategoryStore } from '@/stores/categoryStore'
import { useRouter, useRoute } from 'vue-router'
const router = useRouter()
const route = useRoute()
let lastSlug = route.query.slug

const onPopState = () => {
  const currentSlug = router.currentRoute.value.query.slug
  if (lastSlug === currentSlug) {
    // 防止無窮迴圈
    setTimeout(() => {
      if (window.history.length > 1) window.history.back()
    }, 0)
  }
  lastSlug = currentSlug
}

onMounted(() => {
  window.addEventListener('popstate', onPopState)
})
onUnmounted(() => {
  window.removeEventListener('popstate', onPopState)
})

// store 實例
const uiStore = useUIStore()
const bookStore = useBookStore()
const categoryStore = useCategoryStore()

</script>
