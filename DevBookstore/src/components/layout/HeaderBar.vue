<template>
  <header id="site-header" class="text-white">
    <nav class="navbar navbar-expand-lg px-4">
      <div class="row w-100">
        <!-- 第一列 -->
        <div class="d-flex justify-content-between">
          <!-- 左側 -->
          <div class="col-md-6">
            <ul class="navbar-nav">
              <li class="nav-item">
                <a class="nav-link" href="#" @click="goHomePage">首頁</a>
              </li>
            </ul>
          </div>
          <!-- 右側 -->
           <div v-if="!authStore.isLoggedIn" class="col-md-6 text-end">
            <ul class="navbar-nav flex-row justify-content-end">              
              <li class="nav-item">
                <a class="nav-link me-3" href="#" @click="uiStore.openLogin">登入 / 註冊</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="/user?page=cart">購物車</a>
              </li>
            </ul>
          </div>
          <div v-else class="col-md-6 text-end">
            <ul class="navbar-nav flex-row justify-content-end">
              <li class="nav-item">
                <a class="nav-link me-3" href="/user?page=member">{{displayName}}, 會員</a>
              </li>
              <li class="nav-item">
                <a class="nav-link me-3" href="/user?page=orders">訂單</a>
              </li>
              <li class="nav-item">
                <a class="nav-link me-3" href="#" @click="handleLogout">登出</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="/user?page=cart">購物車</a>
              </li>
            </ul>
          </div>
        </div>

        <!-- 第二列 -->
        <div v-if="showSearch" class="d-flex align-items-center mt-3 mb-3">
          <!-- 左側 logo -->
          <div class="nav-left col-md-2">
            <span class="nav-link">
              <a href="#" class="p-2">LEFT bookstore</a>
            </span>
          </div>
          <!-- 中間 搜尋列 -->
          <div class="nav-fill col-md-8">
            <form class="w-100">
              <div class="input-group">
                <!-- 分類選單 -->
                <select class="form-select" v-model="categoryStore.selectedCategoryId" style="max-width: 120px;">
                  <option v-for="category in headerCategories" :key="category.categoryId" :value="category.categoryId">
                    {{ category.categoryName }}
                  </option>
                </select>
                <!-- 搜尋輸入欄 -->
                <input type="text" class="form-control" v-model="bookStore.searchKeyword" placeholder="搜尋書籍" aria-label="搜尋書籍" />
                <!-- 搜尋按鈕 -->
                <button class="btn btn-warning" @click.prevent="bookStore.fetchBooks({
                  categoryId: categoryStore.selectedCategoryId,
                  keyword: bookStore.searchKeyword
                })">搜尋</button>
              </div>
            </form>
          </div>
          <!-- 右側功能區 -->
          <div class="nav-right col-md-2 text-end">
            <span class="nav-link">
              <a href="#" class="p-2">RIGHT bookstore</a>
            </span>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<script setup>
import { useRouter } from 'vue-router'

// stores
import { useUIStore  } from '@/stores/uiStore.js'
import { useBookStore } from '@/stores/bookStore.js'
import { useCategoryStore } from '@/stores/categoryStore'
import { useAuthStore } from '@/stores/authStore'

// store 實例
const uiStore = useUIStore()
const bookStore = useBookStore()
const categoryStore = useCategoryStore()
const authStore = useAuthStore()

const router = useRouter()
const showSearch = true;
const displayName = localStorage.getItem('displayName');


defineProps({
  headerCategories: Array
})
// 回首頁
const goHomePage = () => {
  bookStore.goHome()
  router.push('/')
}

// 登出
const handleLogout = () => {
  authStore.logout()
  alert('已成功登出')
  // 可選擇導頁，例如回首頁
  window.location.href = '/'
}
</script>

<style scoped>
#site-header {
  background-color: #3e4551;
}

#site-header a {
  position: relative; 
  text-decoration: none;
  color: white; 
  transition: 0.2s;
}

#site-header a:hover {
  color: rgb(227, 144, 0);
}

#site-header a::after {
  align-items: center;
  left: 10%;
  bottom: 2px;
  content: '';
  position: absolute;
  width: 80%;
  height: 3px;
  background-color: #fff;
  border-radius: 5px;
  transform: scale(0);
  transition: transform .3s;
}

#site-header a:hover::after { 
  transform: scale(1);
}

</style>
