<template>
  <div class="container my-4 bg-light p-3">
    <h4 class="mb-3">購物車</h4>

    <!-- 操作區：選擇全部 / 取消勾選 -->
    <div class="mb-3 d-flex justify-content-between align-items-center">
      <div>
        <input type="checkbox" class="form-check-input me-2" :checked="isAllChecked" @change="toggleAll">
        <span>取消/選擇所有商品</span>
      </div>
      <div>
        <button class="btn btn-sm btn-outline-danger" @click="removeSelected">刪除選取</button>
      </div>
    </div>

    <!-- 商品項目 -->
    <div class="card mb-3 p-3" v-for="item in cartStore.userCartItems" :key="item.bookId">
      <div class="row g-3 align-items-center">
        <div class="col-auto">
          <input type="checkbox" class="form-check-input" v-model="item.checked" />
        </div>

        <div class="col-auto">
          <img :src="`${item.bookImages}`" alt="商品圖片"
            class="img-thumbnail" style="width: 120px; height: 160px; object-fit: contain;" />
        </div>

        <div class="col">
          <h5 class="mb-1">{{ item.bookTitle }}</h5>
          <p class="mb-0 text-success fw-bold">現貨</p>
          <p class="mb-0">免費配送至台灣</p>
          <small class="text-muted">格式：紙本書</small>
        </div>

        <div class="col-12 col-md-3 text-end">
          <div class="fw-bold fs-5 text-danger">NT${{ item.unitPrice }}</div>

          <div class="input-group input-group-sm mt-2 justify-content-end">
            <button class="btn btn-outline-secondary" @click="decrease(item)">－</button>
            <input type="text" class="form-control text-center" :value="item.quantity" readonly style="max-width: 60px;">
            <button class="btn btn-outline-secondary" @click="increase(item)">＋</button>
          </div>

          <div class="mt-2">
            <button class="btn btn-link btn-sm text-danger px-1">刪除</button>
            <button class="btn btn-link btn-sm px-1">下次再買</button>
          </div>
        </div>
      </div>
    </div>
    <!-- 結帳資訊區塊 -->
    <div class="p-4 mt-4" style="max-width: 500px; margin-left: auto;">
      <div class="d-flex justify-content-between mb-2 text-muted">
        <div>共 {{ totalQuantity }} 件商品</div>
        <div>商品金額</div>
        <div class="text-dark">NT${{ totalAmount }}</div>
      </div>
      <div class="d-flex justify-content-between mb-2 text-muted">
        <div></div>
        <div>運費</div>
        <div class="text-muted">未選擇</div>
      </div>

      <hr />

      <div class="d-flex justify-content-between align-items-center">
        <div class="fw-bold">小計</div>
        <div class="fw-bold fs-4 text-danger">NT${{ totalAmount }}</div>
      </div>
    </div>

    <!-- 按鈕區 -->
    <div class="d-flex justify-content-end gap-3 mt-3">
      <button class="btn btn-outline-secondary px-4 py-2">繼續購物</button>
      <button class="btn btn-warning px-4 py-2">前往結帳</button>
    </div>


  </div>
</template>

<script setup>

import { useCartStore } from '@/stores/cartStore'
import { onMounted } from 'vue'

const cartStore = useCartStore()
onMounted(() => {
  if (!cartStore.useCartStore) {
    cartStore.fetchCartItems()
  }
})
import { ref, computed } from 'vue'


const increase = (item) => item.quantity++
const decrease = (item) => {
  if (item.quantity > 1) item.quantity--
}

const totalQuantity = computed(() =>
  cartStore.userCartItems.reduce((sum, item) => sum + item.quantity, 0)
)

const totalAmount = computed(() =>
  cartStore.userCartItems.reduce((sum, item) => sum + item.unitPrice * item.quantity, 0)
)

const isAllChecked = computed(() =>
  cartStore.userCartItems.length > 0 &&
  cartStore.userCartItems.every(item => item.checked)
)
const toggleAll = () => {
  const target = !isAllChecked.value
  cartStore.userCartItems.forEach(item => {
    item.checked = target
  })
}
</script>