<template>
  <!-- 全域應用的容器 -->
  <router-view />
</template>

<script setup>
import { onMounted } from 'vue'
import { useCategoryStore } from '@/stores/categoryStore'
import { useUIStore } from '@/stores/uiStore'
import { delay } from '@/utils/delay'
import { useAuthStore } from '@/stores/authStore'

const authStore = useAuthStore()
const categoryStore = useCategoryStore()
const uiStore = useUIStore()

onMounted(async () => {
  authStore.checkLoginStatus()
  uiStore.loadingMap.auth = true
  try {
      // 模擬延遲 0.5 秒
      await delay(500)
      await categoryStore.fetchCategories()
  } catch (err) {
      console.log("selectBook 錯誤", err)
  } finally {
      // 隱藏 loading
      uiStore.loadingMap.auth = false
  }
})
</script>

<style>
/* 全域樣式可以放這，也可以改放在 assets/css/ 內由 main.js 引入 */
body {
  font-family: 'Helvetica', 'Arial', sans-serif;
  margin: 0;
  padding: 0;
}
</style>
