<template>
  <div class="row">
    <div class="book-detail-img col-4">
      <img :src="book.coverImage" alt="書籍封面" />
    </div>
    <div class="book-detail col-6">
      <p>書名：{{ book.title }}</p>
      <p>作者：{{ book.authorName }}</p>
      <p>分類：{{ book.categoryName }}</p>
      <p>價格：${{ book.price }} 元</p>
      <div class="book-detail-btn">
        <button class="btn btn-outline-dark me-3">直接購買</button>
        <button class="btn btn-warning">加入購物車</button>
      </div>
    </div>
    
  </div>
</template>

<script setup>
defineProps({
  book: Object,
})




</script>

<style scoped>
.book-detail-img{

}

.book-detail{
  font-size: 30px;
}

.book-detail-btn{

}
</style>