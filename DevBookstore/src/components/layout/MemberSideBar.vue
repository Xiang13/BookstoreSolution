<template>
  <div class="col-md-2">
    <!-- 側邊欄 -->
    <aside id="sidebar">
        <!-- 導覽列 -->
        <ul class="sidebar-nav p-0">
            <li class="sidebar-header"></li>
            <li
                v-for="item in sidebarCategories"
                :key="item.key"
                class="list-group-item category"
                :class="{ active: isActive(item.key) }"
            >
                <router-link
                class="sidebar-link"
                :to="`/member/${item.key}`"
                >
                <a href="#" class="sidebar-link">
                    {{ item.categoryName }}
                </a>
                </router-link>
            </li>
        </ul>
    </aside>
  </div>

</template>

<script setup>
import { useRoute } from 'vue-router'

const props = defineProps({
  sidebarCategories: {
    type: Array,
  }
})

const route = useRoute()

const isActive = (key) => route.path.includes(key)
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
