<template>
<div class="card mb-5 w-50">
    <div class="card-header text-dark">
        會員基本資料
    </div>
    <div class="card-body">
        <!-- 顯示或編輯狀態切換 -->
        <div v-for="item in profile" class="d-flex justify-content-between align-items-center mb-2">
            <strong>{{ item }}：
                
            </strong>
            <!-- 編輯 / 確認按鈕 -->
                      
        </div>
        <button class="btn btn-outline-dark btn-sm">變更密碼</button>
    </div>
  </div>
</template>

<script setup>
defineProps({
  profile: Object,
})
</script>