<template>
  <!-- 詳細資料畫面 -->
  <div v-if="orderStore.userOrderDetail" class="card shadow-sm px-4 py-3">
    <div class="d-flex justify-content-between align-items-center mb-3">
      <h5 class="text-primary">訂單編號：{{ orderStore.userOrderDetail.orderId }}</h5>
      <button class="btn btn-sm btn-outline-secondary" @click="orderStore.backToList">返回訂單列表</button>
    </div>

    <p>狀態：{{ orderStore.userOrderDetail.orderStatus }} / 付款：{{ orderStore.userOrderDetail.paymentStatus }}</p>
    <p>取貨方式：{{ orderStore.userOrderDetail.pickupMethod }}</p>
    <p>總金額：NT$ {{ orderStore.userOrderDetail.totalAmount }}</p>

    <hr />

    <h6>訂單明細</h6>
    <table class="table table-bordered">
      <thead>
        <tr>
          <th>商品名稱</th>
          <th>數量</th>
          <th>單價</th>
          <th>小計</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in orderStore.userOrderDetail.orderDetails" :key="item.orderDetailId">
          <td>
            <div class="d-flex gap-3 flex-wrap">
              <img
                :src="`/assets/images/${item.productImage}`"
                class="img-thumbnail"
                style="width: 120px; height: 140px; object-fit: contain"
                alt="商品圖片"
              />
              {{ item.bookTitle }}
            </div>
          </td>
          <td>{{ item.quantity }}</td>
          <td>NT$ {{ item.unitPrice }}</td>
          <td>NT$ {{ item.subtotal }}</td>
        </tr>
      </tbody>
    </table>
  </div>



  <div v-else>
    <div class="card shadow-sm mb-4 px-4 py-3" v-for="order in orderStore.userOrders" :key="order.orderId">
      <!-- 訂單編號 + 狀態 -->
      <div class="d-flex justify-content-between align-items-center mb-3 border-bottom pb-2">
        <div>
          <strong>訂單編號：</strong>
          <span class="text-primary fw-bold fs-5">{{ order.orderId }}</span>
        </div>
        <div class="text-danger fw-bold">
          {{ order.paymentStatus }}
        </div>
      </div>

      <!-- 商品圖片列表 -->
      <div class="d-flex gap-3 flex-wrap">
        <img
          v-for="(img, index) in order.bookImages"
          :key="index"
          :src="img"
          class="img-thumbnail"
          style="width: 120px; height: 140px; object-fit: contain"
          alt="商品圖片"
        />
      </div>

      <!-- 數量與金額 -->
      <div class="d-flex justify-content-end align-items-center mb-2">
        <small class="text-muted">共 {{ order.totalQuantity }} 件商品</small>
      </div>
      <div class="d-flex justify-content-end align-items-center mb-2">
          <div class="fs-5 fw-bold">NT$ {{ order.totalAmount }}</div>
      </div>

      <!-- 操作按鈕區 -->
      <div class="d-flex justify-content-end gap-2 border-top pt-3">
        <button class="btn btn-outline-secondary btn-sm" @click="orderStore.fetchOrderDetail(order.orderId)">詳細資料</button>
      </div>
    </div>
  </div>



</template>

<script setup>

import { useOrderStore } from '@/stores/orderStore'
import { onMounted } from 'vue'

const orderStore = useOrderStore()
onMounted(() => {
  if (!orderStore.userOrderDetail) {
    orderStore.fetchOrderList()
  }
})
</script>