<template>
  <div>
    <HeaderBar
      :header-categories="headerCategories"
      :show-search="true"
      @login-click="showLogin = true"
      @home-click="goHome"
      @select-category="selectCategory"
    />
    <div class="container">
      <div class="row mt-4">
        <SideBar
          class="col-md-2"
          :sidebar-categories="sidebarCategories"
          :current-category="currentTab"
          @select-category="selectCategory"
        />

        <div class="col-md-10">
          
          
        </div>
      </div>
    </div>


    <FooterBar />
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from '@/plugins/axios'

// layout
import HeaderBar from '@/components/layout/HeaderBar.vue'
import SideBar from '@/components/layout/SideBar.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

// models
import LoginModal from '@/components/modals/LoginModal.vue'
</script>