<template>
  <div id="bookList" class="row">
    <section
      class="carousel-container my-5"
      v-for="(category, index) in categories"
      :key="index"
    >
      <div class="carousel-header d-flex justify-content-between align-items-center">
        <h4 class="mt-5">{{ category.categoryName }}</h4>
        <a href="#" @click.prevent="bookStore.selectCategory(category)">See All</a>
      </div>
      <div class="card-container">
        <div class="card-wrapper swiper" :ref="el => swiperRefs[index] = el">
          <ul class="card-list swiper-wrapper">
            <li
              class="card-item swiper-slide"
              v-for="book in carouselBooksMap[category.categoryName]"
              :key="book.id"
              @click="bookStore.selectBook(book)">
              <a href="#" class="card-link">
                <img :src="book.coverImage" alt="Card Image" class="card-image" />
                <h2 class="card-title">{{ book.title }}</h2>
              </a>
            </li>
          </ul>
          <div class="swiper-pagination"></div>
          <div class="swiper-slide-button swiper-button-prev"></div>
          <div class="swiper-slide-button swiper-button-next"></div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
import { onMounted, watch, nextTick, ref } from 'vue'
import Swiper from 'swiper/bundle'
import 'swiper/css/bundle'

// store
import { useBookStore } from '@/stores/bookStore'

// store 實例
const bookStore = useBookStore()


const props = defineProps({
  categories: Array,
  carouselBooksMap: Object
})

const swiperRefs = ref([])

const initSwipers = () => {
  props.categories.forEach((cat, index) => {
    const container = swiperRefs.value[index]
    if (!container || container.swiper){
      console.log("initswiper false")
      return
    }
    

    const paginationEl = container.querySelector('.swiper-pagination')
    const nextEl = container.querySelector('.swiper-button-next')
    const prevEl = container.querySelector('.swiper-button-prev')

    new Swiper(container, {
      loop: true,
      spaceBetween: 20,
      pagination: {
        el: paginationEl,
        clickable: true,
        dynamicBullets: true,        
      },
      navigation: {
        nextEl: nextEl,
        prevEl: prevEl
      },
      breakpoints: {
        0: { slidesPerView: 1 },
        768: { slidesPerView: 3 },
        1024: { slidesPerView: 5 }
      }
    })
  })
}

watch(
  () => props.carouselBooksMap,
  () => {
    nextTick(() => {
      setTimeout(() => {
        initSwipers()
      }, 50)
    })
  },
  { deep: true }
)

onMounted(() => {
  initSwipers()
})
</script>

<style scoped>
.card-container .card-wrapper {
  padding-right: 75px;
}

.card-list .card-item .card-link {
  width: 200px;
  display: block;
  text-decoration: none;
}

.card-list .card-link .card-image {
  width: 100%;
  height: 200px;
}

.card-list .card-link .card-title {
  margin: 10px 0px;
  padding: 10px;
  font-size: 16px;
  text-align: center;
}

.card-wrapper .swiper-slide-button {
  margin-top: -60px;
  margin-left: 5px;
  color: rgba(0, 0, 0, 0.202);
  transition: 0.3s;
}

.card-wrapper .swiper-slide-button:hover {
  color: rgb(227, 144, 0);
}

@media screen and (max-width: 768px) {
  .card-wrapper {
    margin: 0 10px 25px;
  }

  .card-wrapper .swiper-slide-button {
    display: none;
  }
}
</style>
