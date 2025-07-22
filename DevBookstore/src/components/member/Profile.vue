<template>
  <div class="card shadow-sm mb-5 mx-5" style="max-width: 600px;">
    <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
      <span><i class="bi bi-person-circle me-2"></i>會員基本資料</span>
    </div>
    <div class="card-body">
      <ul class="list-group list-group-flush mb-3">
        <li class="list-group-item d-flex justify-content-between">
          <strong>姓名</strong>
          <span>{{ authStore.userProfile.displayName }}</span>
        </li>
        <li class="list-group-item d-flex justify-content-between">
          <strong>Email</strong>
          <span>{{ authStore.userProfile.email }}</span>
        </li>
        <li class="list-group-item d-flex justify-content-between">
          <strong>電話號碼</strong>
          <span>{{ authStore.userProfile.phoneNumber }}</span>
        </li>
        <li class="list-group-item d-flex justify-content-between">
          <strong>地址</strong>
          <span>{{ authStore.userProfile.address }}</span>
        </li>
        <li class="list-group-item d-flex justify-content-end">
          <button class="btn btn-outline-dark btn-sm">
            <i></i>變更密碼
          </button>
        </li>        
      </ul>

      <div class="text-end">
        
      </div>
    </div>
  </div>
</template>

<script setup>

import { useAuthStore } from '@/stores/authStore'
const authStore = useAuthStore()
</script>