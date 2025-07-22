<template>
  <div>
    <!-- 頁首 -->
    <HeaderBar :header-categories="categoryStore.headerCategories"/>
    <div class="container">
      <div class="row mt-4">
        <!-- 側邊欄 -->
         <SideBar
            v-model="bookStore.bookCurrentTab"
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
import { delay } from '@/utils/delay'

import { useRouter, useRoute } from 'vue-router'
import { onMounted, onUnmounted, watch } from 'vue'
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

// store 實例
const uiStore = useUIStore()
const bookStore = useBookStore()
const categoryStore = useCategoryStore()

onMounted(async () => {
  uiStore.loadingMap.books = true
  try {
      // 模擬延遲 0.5 秒
      await delay(500)       
      await categoryStore.fetchCarouselBooks()
      await bookStore.initRouteWatcher()
  } catch (err) {
      console.log("BookPage 錯誤", err)
  } finally {
      // 隱藏 loading
      uiStore.loadingMap.books = false
  }    
})
// onUnmounted(() => {
//   window.removeEventListener('popstate', onPopState)
// })

</script>
