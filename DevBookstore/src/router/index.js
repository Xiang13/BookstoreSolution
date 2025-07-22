import { createRouter, createWebHistory } from 'vue-router'
import BookPage from '@/pages/BookPage.vue'
import MemberPage from '@/pages/MemberPage.vue'

import Profile from '@/components/member/Profile.vue'
import Orders from '@/components/member/Orders.vue'
import Cart from '@/components/member/Cart.vue'

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
      { path: '', redirect: 'profile' },
      { path: 'profile', component: Profile },
      { path: 'orders', component: Orders },
      { path: 'cart', component: Cart },
    ], },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.afterEach((to, from) => {
  console.log('路由變化', from.fullPath, '→', to.fullPath)
})

let previousFullPath = null
let repeatCount = 0

router.afterEach((to, from) => {
  if (to.fullPath === from.fullPath && from.fullPath === previousFullPath) {
    repeatCount++

    console.warn(`[Router] 🔁 第 ${repeatCount} 次重複導航: ${from.fullPath}`)

    if (to.name === 'booksByCategory' && repeatCount >= 2) {
      console.warn('[Router] ⬅️ 自動返回一次，修正歷史堆疊')
      repeatCount = 0  // reset
      history.back()
    }
    return
  }

   // 正常路由變化
  console.log(`[Router] ✅ 路由變化: ${from.fullPath} → ${to.fullPath}`)
  previousFullPath = to.fullPath
  repeatCount = 0
})

export default router

// cd C:\GitRepos\BookstoreSolution\DevBookstore