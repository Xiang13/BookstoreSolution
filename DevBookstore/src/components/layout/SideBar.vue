<template>
  <div class="col-md-2">
    <!-- 側邊欄 -->
    <aside id="sidebar">
      <!-- 導覽列 -->
      <ul class="sidebar-nav p-0">
        <li class="sidebar-header"></li>
        <li
          v-for="(item, index) in sidebarCategories"
          :key="index"
          class="list-group-item category"
          :class="{ active: item[keyField] === modelValue }"
          @click="updateItemValue(item[keyField])"
        >
          <a href="#" class="sidebar-link">
            {{ item[labelField] }}
          </a>
        </li>
      </ul>
    </aside>
  </div>
</template>

<script setup>

const props = defineProps({
  // 所有選項
  sidebarCategories: Array,
  // v-model 綁定的資料
  modelValue: [String, Number],
  // 例如 'categoryId' 或 'key'
  keyField: String,
  // 例如 'categoryName' 或 'name'
  labelField: String,
  // 當項目被選中時的 callback
  onItemSelected: Function
})
const emit = defineEmits(['update:modelValue'])
const updateItemValue = (key) => {
  emit('update:modelValue', key)
  props.onItemSelected?.(key)
}

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
