import { watch, computed, ref } from 'vue'
import { defineStore } from 'pinia'
import { useRouter, useRoute } from 'vue-router'
import axios from 'axios'
import { delay } from '@/utils/delay'

// stores
import { useCategoryStore } from '@/stores/categoryStore'
import { useUIStore } from '@/stores/uiStore'

export const useBookStore = defineStore('book', () => {
    // store 實例
    const uiStore = useUIStore()
    const categoryStore = useCategoryStore()

    // 所有書籍（可根據分類載入）
    const books = ref([])
    // 點選的書籍
    const selectedBook = ref(null)
    // 當前分類名稱
    const bookCurrentTab = ref(null)
    // 分類名稱或搜尋關鍵字
    const searchKeyword = ref('')
    // 篩選後書籍資料
    const filteredBooks = ref([])
    // 查無結果
    const noResult = ref(false)
    // 路由控制與狀態判斷
    const route = useRoute()
    const router = useRouter()

    const isHome = computed(() => route.name === 'home')
    const isBookListView = computed(() =>
        !isHome.value && !selectedBook.value
    )

    const isCarouselView = computed(() =>
        isHome.value && !selectedBook.value
    )

    const isBookDetailView = computed(() =>
        !!selectedBook.value
    )

    
    const initRouteWatcher = () => {
        const slug = route.params.slug
        if (slug !== undefined) {
            const category = categoryStore.sidebarCategories.find(c => c.slug === slug)
            if (category) {
                loadBooksBySlug(category)
            }
        }
        watch(() => route.params.slug, async (newSlug, oldSlug) => {
            if (newSlug === oldSlug) {
                console.log('[Books] slug 沒變，略過載入')
                window.history.back()
                return
            }
            if (newSlug !== undefined) {
                const category = categoryStore.sidebarCategories.find(c => c.slug === newSlug)
                await loadBooksBySlug(category)
            }
        },{ immediate: true })
    }
    // 根據 slug 載入對應分類書籍
    const loadBooksBySlug = async (category) => {
        const categoryId = category.categoryId || -3
        const keyword = searchKeyword.value || ''
        try {
            await fetchBooks({ categoryId, keyword })
        } catch (err) {
            console.error('loadBooksBySlug 錯誤', err)
        }
    }

    // 根據選擇的 categoryId 切換書籍分類
    const handleBookCategoryChange = (categoryId) => {
        console.log('[BookPage] 切換到:', categoryId)
        bookCurrentTab.value = null
        // 從 categoryStore 找出對應的名稱
        const category = categoryStore.sidebarCategories.find(c => c.categoryId === categoryId)                
        // 對應到 slug，若找不到預設為 all
        const slug = category.slug || 'all'
        // // 更新 URL        
        if (route.params.slug !== slug) {
            router.replace({ name: 'booksByCategory', params: { slug } })
        } else {
            // slug 沒變，手動重新載入
            loadBooksBySlug(category)
        }        
    }

    // 回首頁
    const goHome = async () => {
        console.log("goHome start")
        try {
            uiStore.loadingMap.books = true
            resetBookPageState()
            // 模擬延遲 0.5 秒
            await delay(500)
        }
        catch (err) {
            console.error("goHome 發生錯誤", err)
        } finally {
            // 隱藏 loading
            uiStore.loadingMap.books = false
        }
    }

    // 依分類或關鍵字載入書籍清單
    const fetchBooks = async ({ categoryId = null, keyword = '' } = {}) => {
        try {            
            uiStore.loadingMap.books = true
            const params = {}
            if (categoryId) params.CategoryId = categoryId
            if (keyword) params.Keyword = keyword
            bookCurrentTab.value = categoryId
            const res = await axios.get('/Book/books', { params })
            // 模擬延遲 0.5 秒
            await delay(500)

            filteredBooks.value = res.data.map(b => ({
                ...b,
                coverImage: `/assets/images/${b.coverImage}`
            }))
            // 如果搜尋結果為 0 時
            noResult.value = filteredBooks.value.length === 0
            
            window.scrollTo(0, 0)
            searchKeyword.value = null
            selectedBook.value = null
            categoryStore.selectedCategoryId = categoryStore.headerCategories[0].categoryId
            // console.log("篩選資料", filteredBooks.value)
        } catch (err) {
            console.error('無法載入書籍', err)
        } finally {
            // 隱藏 loading
            uiStore.loadingMap.books = false
        }
    }

    // 設定目前選中的書籍，用於顯示書籍詳細資料
    const selectBook = async (book) => {
        try {
            uiStore.loadingMap.books = true
            selectedBook.value = book
            // 模擬延遲 0.5 秒
            await delay(500)
        } catch (err) {
            console.log("selectBook 錯誤", err)
        } finally {
            // 隱藏 loading
            uiStore.loadingMap.books = false
        }
    }

    // 回首頁 清空資料
    const resetBookPageState = () => {
        searchKeyword.value = null
        bookCurrentTab.value = null
        selectedBook.value = null

        // 重設分類下拉選單為第一項（如果有）
        if (categoryStore.headerCategories.length > 0) {
            categoryStore.selectedCategoryId = categoryStore.headerCategories[0].categoryId
        }
    }


    return {
        books,
        selectedBook,
        bookCurrentTab,
        searchKeyword,
        filteredBooks,
        noResult,
        isHome,
        isCarouselView,
        isBookListView,
        goHome,
        fetchBooks,
        selectBook,
        initRouteWatcher,
        loadBooksBySlug,
        handleBookCategoryChange,
        resetBookPageState,
    }
})