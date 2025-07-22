import { defineStore } from 'pinia'
import { useUIStore } from '@/stores/uiStore'

import { ref } from 'vue'
import { delay } from '@/utils/delay'

import axios from 'axios'

export const useOrderStore = defineStore('order', () => {
  // 多筆訂單
  const userOrders = ref([])
  // 訂單詳細資料
  const userOrderDetail = ref(null)

  const uiStore = useUIStore()

 
  const fetchOrderList = async () => {
    try {
      const res = await axios.get('/Member/orders', {
        withCredentials: true
      })
      const orders = res.data
      userOrders.value = orders.map(order => ({
      orderId: order.orderId,
      orderDate: order.orderDate,
      totalAmount: order.totalAmount,
      totalQuantity: order.totalQuantity,
      orderStatus: order.orderStatus,
      paymentStatus: order.paymentStatus,
      pickupMethod: order.pickupMethod,
      bookImages: order.bookImages.map(img => `/assets/images/${img}`)
    }))
      console.log("訂單資訊", userOrders.value)
    }
    catch (err) {
      console.error('取得訂單時發生錯誤', err)
    }
  }
  const fetchOrderDetail = async (orderId) => {
    uiStore.loadingMap.user = true
    await delay(500)
    try {
      const res = await axios.get('/Member/orderDetail', {
        params: { orderId },
        withCredentials: true
      })
      console.log("res", res)
      const order = res.data
      userOrderDetail.value = {
        orderId: order.orderId,
        orderDate: order.orderDate,
        totalAmount: order.totalAmount,
        totalQuantity: order.totalQuantity,
        orderStatus: order.orderStatus,
        paymentStatus: order.paymentStatus,
        pickupMethod: order.pickupMethod,
        orderDetails: order.orderDetails || []
      }
      console.log("訂單詳細資訊", order)
    }
    catch (err) {
      console.error('取得訂單詳細時發生錯誤', err)
    } finally {
      uiStore.loadingMap.user = false
    }
  }
  const backToList = () => {
    userOrderDetail.value = null
    fetchOrderList() 
  }
  return {
    userOrders,
    userOrderDetail,
    fetchOrderList,
    fetchOrderDetail,
    backToList,
  }
})