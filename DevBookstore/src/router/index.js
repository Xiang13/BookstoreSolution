import { createRouter, createWebHistory } from 'vue-router'
import BookPage from '@/pages/BookPage.vue'
import MemberPage from '@/pages/MemberPage.vue'

const routes = [
  { path: '/',
    name: 'home',
    component: BookPage
  },
  {
    path: '/books/:slug?',
    name: 'booksByCategory',
    component: BookPage
  },
  { path: '/member',
    component: MemberPage,
    children: [
      { path: '', redirect: 'Member' },
      { path: 'Member', component: () => import('@/components/member/Member.vue') },
      { path: 'cart', component: () => import('@/components/member/Cart.vue') },
      { path: 'orders', component: () => import('@/components/member/Orders.vue') },
    ], },
]

export default createRouter({
  history: createWebHistory(),
  routes
})

// cd C:\GitRepos\BookstoreSolution\DevBookstore