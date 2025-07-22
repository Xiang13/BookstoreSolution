import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src')
    }
  },
  server: {
    proxy: {
      '/Book': {
        target: 'https://localhost:7023', // 你的 ASP.NET Core 後端 API 網址
        changeOrigin: true,
        secure: false // 如果使用自簽憑證，需設定為 false
      }
    }
  }
})