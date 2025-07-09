<template>
  <div id="bookList" class="row">
    <div v-if="bookStore.noResult" class="text-center">
      <h2 class="col-md-4 my-5">查無符合條件的書籍</h2>
    </div>
    <div v-else
      class="col-md-4 my-5"
      v-for="book in books"
      :key="book.id"
      @click="bookStore.selectBook(book)"
    >
      <div class="card h-100">
        <img :src="book.coverImage" class="card-img-top" alt="書籍封面" />
        <div class="card-body">
          <h5 class="card-title">書名：{{ book.title }}</h5>
          <p class="card-text">作者：{{ book.authorName }}</p>
          <p class="card-text">分類：{{ book.categoryName }}</p>
          <p class="card-text text-danger">價格：${{ book.price }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  books: Array,
})
// store
import { useBookStore } from '@/stores/bookStore'

// store 實例
const bookStore = useBookStore()

</script>

<style scoped>
.card-img-top {
  object-fit: cover;
}
</style>
