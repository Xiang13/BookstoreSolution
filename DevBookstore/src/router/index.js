import { createRouter, createWebHistory } from 'vue-router'
import BookPage from '@/pages/BookPage.vue'
import MemberPage from '@/pages/MemberPage.vue'

const routes = [
  { path: '/', component: BookPage },
  { path: '/member', component: MemberPage },
]

export default createRouter({
  history: createWebHistory(),
  routes
})
