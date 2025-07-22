import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'
import BookPage from '@/pages/BookPage.vue'
import MemberPage from '@/pages/MemberPage.vue'
import { useCategoryStore } from '@/stores/categoryStore'

const routes = [
  { path: '/',
    // name: 'home',
    component: BookPage
  },
  // {
  //   path: '/books/:slug?',
  //   name: 'booksByCategory',
  //   component: BookPage
  // },
  // {
  //   path: '/books',
  //   name: 'booksByCategory',
  //   component: BookPage
  // },
  { path: '/member',
    component: MemberPage,
    children: [
      { path: '', redirect: 'Member' },
      { path: 'profile', component: () => import('@/components/member/Profile.vue') },
      { path: 'cart', component: () => import('@/components/member/Cart.vue') },
      { path: 'orders', component: () => import('@/components/member/Orders.vue') },
    ], },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  // 只在首頁且沒有 slug 時導向預設分類
  // if (to.path === '/' && !to.query.slug) {
  //   // 這裡不能直接用 store，需等 store 初始化
  //   const categoryStore = useCategoryStore()
  //   if (categoryStore.sidebarCategories.length > 0) {
  //     const defaultCategory = categoryStore.sidebarCategories[0]
  //     next({ path: '/', query: { slug: defaultCategory.slug } })
  //     return
  //   }
  // }
  // if(from.fullPath === to.fullPath){
  //   window.history.back()
  // }
  next()
})

router.afterEach((to, from) => {
  console.log('路由變化', from.fullPath, '→', to.fullPath)
})

export default {
  router,
}

// export default  createRouter({
//   history: createWebHistory(),
//   routes
// })



// cd C:\GitRepos\BookstoreSolution\DevBookstore