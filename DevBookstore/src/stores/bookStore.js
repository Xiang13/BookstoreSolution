import { ref } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'

// stores
import { useCategoryStore } from '@/stores/categoryStore'
import { useUIStore } from '@/stores/uiStore'



export const useBookStore = defineStore('book', () => {
    // store 實例
    const uiStore = useUIStore()
    // 所有書籍（可根據分類載入）
    const books = ref([])
    // 點選的書籍
    const selectedBook = ref(null)
    // 當前分類名稱
    const currentTab = ref(null)
    // 分類名稱或搜尋關鍵字
    const searchKeyword = ref('')
    // 篩選後書籍資料
    const filteredBooks = ref([])
    // 延遲
    const delay = (ms) => new Promise(resolve => setTimeout(resolve, ms))

    // 回首頁
    const goHome = async  () => {
        console.log("goHome start")
        try {
            uiStore.loadingMap.books = true
            searchKeyword.value = null
            currentTab.value = null
            selectedBook.value = null
            
            // 下拉選單回到預設值
            const categoryStore = useCategoryStore()
            if (categoryStore.headerCategories.length > 0) {
                categoryStore.selectedCategoryId = categoryStore.headerCategories[0].categoryId
            }
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
        const isSameCategory = categoryId === currentTab.value?.categoryId
        const isSameKeyword = keyword === currentTab.value?.keyword     

        // 分類沒變就不重新載入
        if (isSameCategory && isSameKeyword) {
            console.log('分類與關鍵字皆未變，略過請求')
            return
        }
        try {
            // isLoading.value = true
            uiStore.loadingMap.books = true
            const params = {}
            if (categoryId) params.CategoryId = categoryId
            if (keyword) params.Keyword = keyword
            const res = await axios.get('/Book/books', { params })

            // 模擬延遲 0.5 秒
            await delay(500)

            filteredBooks.value = res.data.map(b => ({
                ...b,
                coverImage: `/assets/images/${b.coverImage}`
            }))
            currentTab.value = categoryId
            selectedBook.value = null
            window.scrollTo(0, 0)
            searchKeyword.value = null
            // console.log("篩選資料", filteredBooks.value)
        } catch (err) {
            console.error('無法載入書籍', err)
        } finally {
            // 隱藏 loading
            uiStore.loadingMap.books = false
        }
    }

    // 設定目前選中的書籍，用於顯示詳細資料
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

    return {
        books,
        selectedBook,
        currentTab,
        searchKeyword,
        filteredBooks,
        goHome,
        fetchBooks,
        selectBook,
    }
})