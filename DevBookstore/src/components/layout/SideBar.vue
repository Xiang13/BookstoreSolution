<template>
  <div class="col-md-2">
    <!-- 側邊欄 -->
    <aside id="sidebar">
      <!-- 導覽列 -->
      <ul class="sidebar-nav p-0">
        <li class="sidebar-header"></li>
        <li
          v-for="(category, index) in sidebarCategories"
          :key="index"
          class="list-group-item category"
          :class="{ active:  bookStore.currentTab === category.categoryId }"
          @click="bookStore.fetchBooks(category)"
        >
          <a href="#" class="sidebar-link">
            {{ category.categoryName }}
          </a>
        </li>
      </ul>
    </aside>
  </div>
</template>

<script setup>
defineProps({
  sidebarCategories: Array
})

// store
import { useBookStore } from '@/stores/bookStore'

// store 實例
const bookStore = useBookStore()

</script>

<style scoped>
#sidebar {
  width: 100%;
}

.sidebar-nav a {
  text-decoration: none;
  color: black;
}

.sidebar-header {
  list-style: none;
  width: 100px;
  height: 100px;
}

a.sidebar-link {
  padding: 0.625rem 1.625rem;
  display: block;
}

.list-group-item {
  transition: all 0.2s;
}

.list-group-item:hover {
  background-color: #03020230;
}

.sidebar-nav li.active {
  background-color: #f0ad4e;
}
</style>
