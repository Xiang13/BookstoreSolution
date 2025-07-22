import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from '@/utils/axios'

export const useCartStore = defineStore('cart', () => {

  // 購物車詳細資料
    const userCartItems = ref([])

  const fetchCartItems = async () => {    
    try {
      const res = await axios.get('/Member/cartItems', {
        withCredentials: true
      })
      const items = res.data
      userCartItems.value = items.map(item => ({
      cartId: item.cartId,
      bookId: item.bookId,
      bookTitle: item.bookTitle,
      quantity: item.quantity,
      totalAmount: item.totalAmount,
      unitPrice: item.unitPrice,
      subTotal: item.subTotal,
      bookImages: `/assets/images/${item.productImage}`,
    }))
      console.log("購物車資訊", userCartItems.value)
    }
    catch (err) {
      console.error('取得購物車時發生錯誤', err)
    }
  }


  return {
    fetchCartItems,
    userCartItems,
  }
})
