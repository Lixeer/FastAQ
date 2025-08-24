import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'
import path from 'node:path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  server : {
    host : true,
    proxy : {
      "/api":{
        target:"http://111.170.155.88:5000",
        changeOrigin : true,
        rewrite : (path) => path.replace("/api/","/")
      }
    }
  }
})
