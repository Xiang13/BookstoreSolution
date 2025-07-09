import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'

export const useCategoryStore = defineStore('category', () => {
  // 所有分類原始資料
    const categories = ref([])
    // Header 的分類
    const headerCategories = ref([])
    // Sidebar 的分類
    const sidebarCategories = ref([])
    // Carousel 的分類
    const carouselCategories = ref([])
    // footer 的分類
    const footerCategories = ref([])
    // 輪播的每個分類對應的書籍（Map 結構）
    const carouselBooks = ref({})
    // 下拉式選單選項
    const selectedCategoryId = ref(null)

    // 載入分類資料
    const fetchCategories = async () => {
        try {
            const res = await axios.get('/Book/categories')
            categories.value = res.data.sort((a, b) => a.categoryId - b.categoryId)            
            // 頁首分類
            headerCategories.value = categories.value.filter(c => c && (c.categoryId === -3 || c.categoryId > 0))
            // 側邊欄分類
            sidebarCategories.value = categories.value
            // 輪播分類
            carouselCategories.value = categories.value.filter(c => c.categoryId >= -2)
            // 頁尾分類
            footerCategories.value = categories.value.filter(c => c.categoryId >= 0)

            // 預設選擇第一個分類
            if (headerCategories.value.length > 0) {
               selectedCategoryId.value = headerCategories.value[0].categoryId
            }
            // console.log('所有分類', categories.value)
        } catch (err) {
            console.error('載入分類失敗', err)
        }
    }

    // 載入每個輪播分類的書籍
    const fetchCarouselBooks = async () => {
        try {
            const res = await axios.get('/Book/carousel/all')
            const raw = res.data
            const result = {}
            for (const group of raw) {
            const books = group.books.map(book => ({
                ...book,
                coverImage: `/assets/images/${book.coverImage}`
            }))
            result[group.categoryName] = books
            }
            carouselBooks.value = result
            // console.log("輪播資料", carouselBooks.value)

        } catch (err) {
        console.error('載入輪播書籍失敗', err)
        }
    }

    return {
        categories,
        headerCategories,
        sidebarCategories,
        carouselCategories,
        footerCategories,        
        carouselBooks,
        selectedCategoryId,
        fetchCategories,
        fetchCarouselBooks,
    }
})