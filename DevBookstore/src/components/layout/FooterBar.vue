<template>
  <footer class="text-center text-lg-start text-white" style="background-color: #3e4551">
    <div class="navBackToTop">
      <a href="#">回頂端</a>
    </div>

    <div class="container p-4 pb-0">
      <section>
        <div class="row">
          <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
            <!-- 空白佔位區 -->
          </div>

          <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
            <h5 class="text-uppercase">分類選單</h5>
            <ul class="list-unstyled mb-0">
              <li v-for="category in footerCategories"
                :key="category.categoryId">
                <a href="#" @click = bookStore.handleBookCategoryChange(category.categoryId)>
                  {{ category.categoryName }}
                </a>
              </li>
            </ul>
          </div>

          <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
            <h5 class="text-uppercase">會員專區</h5>
            <ul class="list-unstyled mb-0">
              <li><a href="#" @click="memberStore.handleMemberTabChange('member')">個人會員</a></li>
              <li><a href="#" @click="memberStore.handleMemberTabChange('orders')">訂單</a></li>
              <li><a href="#" @click="memberStore.handleMemberTabChange('cart')">購物車</a></li>
            </ul>
          </div>

          <div class="col-lg-3 col-md-6 mb-4 mb-md-0">
            <h5 class="text-uppercase">聯絡我們</h5>
            <ul class="list-unstyled mb-0">
              <li>
                <p>Email: <a href="mailto:bookstore@example.com" class="text-white">bookstore@example.com</a></p>
              </li>
              <li>
                <p>GitHub: <a href="#" class="text-white">github.com/yourusername</a></p>
              </li>
            </ul>
          </div>
        </div>
      </section>

      <hr class="mb-4" />

      <section class="mb-4 text-center">
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-facebook-f"></i></a>
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-twitter"></i></a>
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-google"></i></a>
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-instagram"></i></a>
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-linkedin-in"></i></a>
        <a class="btn btn-outline-light btn-floating m-1" href="#"><i class="fab fa-github"></i></a>
      </section>
    </div>

    <div class="text-center p-3" style="background-color: rgba(0, 0, 0, 0.2)">
      &copy; 2025 Copyright:
      <a class="text-white" href="#">YourBookstore.com</a>
    </div>
  </footer>
</template>

<script setup>
const props = defineProps({
  footerCategories: Array,
  modelValue: [String, Number],
  // 例如 'categoryId' 或 'key'
  keyField: String,
  // 例如 'categoryName' 或 'name'
  labelField: String,
  // 當項目被選中時的 callback
  onItemSelected: Function
})

// store
import { useBookStore } from '@/stores/bookStore'
import { useMemberStore } from '@/stores/memberStore'

// store 實例
const bookStore = useBookStore()
const memberStore = useMemberStore()

const emit = defineEmits(['update:modelValue'])

const updateItemValue = (key) => {
  emit('update:modelValue', key)
  props.onItemSelected?.(key)
}
</script>

<style scoped>
.navBackToTop a {
  padding: 5px;
  display: block;
  background-color: #353b4c;
  color: white;
  text-align: center;
  text-decoration: none;
  width: 100%;
}

.list-unstyled a {
  text-decoration: none;
  transition: all 0.2s;
  color: white;
}

.list-unstyled li a:hover {
  color: rgb(227, 144, 0);
}
</style>
