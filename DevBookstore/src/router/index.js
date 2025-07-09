import { createRouter, createWebHistory } from 'vue-router'
import BookPage from '@/pages/BookPage.vue'
import UserPage from '@/pages/UserPage.vue'

const routes = [
  { path: '/', component: BookPage },
  { path: '/user', component: UserPage },
]

export default createRouter({
  history: createWebHistory(),
  routes
})

// cd C:\GitRepos\BookstoreSolution\DevBookstore