import 'bootstrap/dist/css/bootstrap.min.css'
import 'swiper/css/bundle'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import { createPinia } from 'pinia'

axios.defaults.baseURL = 'https://localhost:7023/api'
createApp(App).use(createPinia()).use(router).mount('#app')