// src/plugins/axios.js
import axios from 'axios'

const instance = axios.create({
  baseURL: 'https://localhost:7023/api',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
})

// 可加攔截器 if needed
export default instance
