<template>
  <div v-if="uiStore.showLogin" class="wrapper" @click.self="uiStore.closeLogin">

    <div v-if="loading" class="form-box loading-box" @click.stop>
      <h3 style="text-align: center;">載入中...</h3>
      <div class="d-flex justify-content-center align-items-center" style="height: 150px;">
        <div class="spinner-border" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>
    <!-- 登入視窗 -->
    <div v-else-if="!isRegistering" class="form-box login" @click.stop>
      <form>
        <p v-if="errors.login" class="text-danger small mt-1">{{ errors.login }}</p>
        <span class="close-button" @click="uiStore.closeLogin">&times;</span>
        <h2>登入</h2>
        <div class="input-box">
          <input v-model="loginData.email" type="email" placeholder=" " />
          <label>Email</label>
          <p v-if="errors.email" class="text-danger small mt-1">{{ errors.email }}</p>
        </div>
        <div class="input-box">
          <input v-model="loginData.password" type="password" placeholder=" " />
          <label>Password</label>
          <p v-if="errors.password" class="text-danger small mt-1">{{ errors.password }}</p>
        </div>
        <div class="remember-forgot mt-3">
          <label><input type="checkbox" v-model="rememberMe" />記住我</label>
          <a href="#">忘記密碼?</a>
        </div>
        <a href="#" @click="userLogin">使用者登入</a>
        <button type="button" class="login-btn" @click="handleLogin" :disabled="loading">
          <span v-if="loading">登入中...</span>
          <span v-else>登入</span>
        </button>
        <div class="login-register">
          <p class="mt-3 text-center">
            還沒有帳號？
            <span class="text-primary" style="cursor: pointer;" @click="switchAuthMode">點此註冊</span>
          </p>
        </div>
      </form>
    </div>

    <!-- 註冊視窗 -->
    <div v-else class="form-box register" @click.stop>
      <span class="close-button" @click="uiStore.closeLogin">&times;</span>
      <h2>註冊</h2>
      <form>
        <div class="input-box">
          <input v-model="registerData.userName" type="text" placeholder="" />
          <label>User Name</label>
          <p v-if="errors.userName" class="text-danger small mt-1">{{ errors.userName }}</p>
        </div>        
        <div class="input-box">
          <input v-model="registerData.email" type="email" placeholder="" />
          <label>Email</label>
          <p v-if="errors.email" class="text-danger small mt-1">{{ errors.email }}</p>
        </div>        
        <div class="input-box">
          <input v-model="registerData.password" type="password" placeholder="" />
          <label>Password</label>
          <p v-if="errors.password" class="text-danger small mt-1">{{ errors.password }}</p>
        </div>        
        <div class="input-box">
          <input v-model="registerData.confirmPassword" type="password" placeholder="" />
          <label>Confirm Password</label>
          <p v-if="errors.confirmPassword" class="text-danger small mt-1">{{ errors.confirmPassword }}</p>
        </div>        
        <div class="remember-forgot mt-3">
          <label><input type="checkbox" v-model="agreeTerms" /> 我已閱讀並同意 會員條款</label>
          <p v-if="errors.agreeTerms" class="text-danger small">{{ errors.agreeTerms }}</p>
        </div>        
        <div class="submit-box">
          <button type="button" class="submit-btn" @click="handleRegister">
            <span v-if="loading">註冊中...</span>
            <span v-else>註冊</span>
          </button>
          <button type="button" class="submit-btn" @click="switchAuthMode" :disabled="loading">取消</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { watch, computed, ref } from 'vue'
import { delay } from '@/utils/delay'
import axios from 'axios'

// store
import { useUIStore } from '@/stores/uiStore'
import { useAuthStore } from '@/stores/authStore'
// store 實例
const uiStore = useUIStore()
const authStore = useAuthStore()
// loading 狀態
const loading = computed(() => uiStore.loadingMap.auth)

const isRegistering = ref(false)
// 登入資料
const loginData = ref({ email: '', password: '' })
// 註冊資料
const registerData = ref({ userName: '', email: '', password: '', confirmPassword: '' })
// 勾選狀態
const rememberMe = ref(false)
const agreeTerms = ref(false)
// 錯誤訊息
const errors = ref({})

// 共用切換模式方法（登入 <-> 註冊）
const switchAuthMode = async () => {
  isRegistering.value = !isRegistering.value
  // 清除所有錯誤
  errors.value = {}
  resetForms()
  uiStore.loadingMap.auth = true
  await delay(500)
  uiStore.loadingMap.auth = false
}
// 在登入視窗開啟時，讀取 localStorage，預填 email
watch(
  () => uiStore.showLogin,
  (isOpen) => {
    if (isOpen) {
      const savedEmail = localStorage.getItem('rememberedEmail')
      if (savedEmail) {
        loginData.value.email = savedEmail
        rememberMe.value = true
      } else {
        loginData.value.email = ''
        rememberMe.value = false
      }
      loginData.value.password = ''
    }
  }
)
// 監聽「記住我」勾選取消，若取消就刪除記住的 email
watch(rememberMe, (checked) => {
  if (!checked) {
    localStorage.removeItem('rememberedEmail')
  }
})
// 登入
const handleLogin = async () => {
  if (!validateLogin()) return  
  try {    
    uiStore.loadingMap.auth = true
    await delay(500)
    const res = await axios.post('/Auth/login', {
      Email: loginData.value.email,
      Password: loginData.value.password,
    })
    
    if (res.status === 200) {
      const { token, message } = res.data
      
      // 儲存 token
      authStore.login(res.data.data.displayName)
      localStorage.setItem('userId', res.data.data.userId)
      localStorage.setItem('displayName', res.data.data.displayName)
      localStorage.setItem('token', token)

      uiStore.closeLogin()
    }
    if (rememberMe.value) {
      localStorage.setItem('rememberedEmail', loginData.value.email)
    } else {
      localStorage.removeItem('rememberedEmail')
    }
    resetForms()
    uiStore.closeLogin()
    window.location.href = '/'

  } catch (err) {
    errors.value.login = err.response?.data?.message || '登入失敗，請再試一次'
    // resetForms()
  } finally {
    uiStore.loadingMap.auth = false
  }
}

// 註冊
const handleRegister = async () => {
  if (!validateRegister()) return
  if (registerData.value.password !== registerData.value.confirmPassword) {
    alert('密碼與確認密碼不一致')
  }
  try {
    uiStore.loadingMap.auth = true
    // 模擬 API
    await delay(500)    
    alert('註冊成功，請登入')
    // 清除所有錯誤
    errors.value = {}
    switchAuthMode()
  } catch (err) {
    alert('註冊失敗')
  } finally {
    uiStore.loadingMap.auth = false
  }
}

// 重設所有表單
const resetForms = () => {
  const savedEmail = localStorage.getItem('rememberedEmail')
  loginData.value = { email:savedEmail || '', password: '' }
  registerData.value = { userName: '', email: '', password: '', confirmPassword: '' }
  agreeTerms.value = false
  // rememberMe.value = false
}

// 驗證登入欄位
const validateLogin = () => {
  errors.value = {}
  if (!loginData.value.email) {
    errors.value.email = '請輸入 Email'
  }
  if (!loginData.value.password) {
    errors.value.password = '請輸入密碼'
  }
  return Object.keys(errors.value).length === 0
}

// 驗證註冊欄位
const validateRegister = () => {
  errors.value = {}

  if (!registerData.value.userName) {
    errors.value.userName = '請輸入使用者名稱'
  }
  if (!registerData.value.email) {
    errors.value.email = '請輸入 Email'
  }
  if (!registerData.value.password) {
    errors.value.password = '請輸入密碼'
  }
  if (!registerData.value.confirmPassword) {
    errors.value.confirmPassword = '請再次輸入密碼'
  }
  if (
    registerData.value.password &&
    registerData.value.confirmPassword &&
    registerData.value.password !== registerData.value.confirmPassword
  ) {
    errors.value.confirmPassword = '密碼與確認密碼不一致'
  }
  if (!agreeTerms.value) {
    errors.value.agreeTerms = '請同意會員條款'
  }

  return Object.keys(errors.value).length === 0
}

// 自動輸入使用者資料
const userLogin = () => {
  loginData.value = { email:'tom@gmail.com', password: '123' }
}
</script>

<style scoped>
.wrapper {
  position: fixed;
  top: 0; left: 0;
  width: 100%; height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex; align-items: center; justify-content: center;
  z-index: 1000;
}

.form-box {
  background: white;
  padding: 30px;
  border-radius: 8px;
  min-width: 500px;
  position: relative;
  box-shadow: 0 0 30px rgba(0, 0, 0, .5);
}

.form-box h2 {
  font-size: 2em;
  color: #162938;
  text-align: center;
}

.input-box {
  position: relative;
  width: 100%;
  height: 50px;
  border-bottom: 2px solid #162938;
  margin: 30px 0;
}

.input-box label {
  position: absolute;
  top: 30%;
  left: .5%;
  pointer-events: none;
  transition: .5s;
}

.input-box input {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  font-size: 1em;
  color: #162938;
  font-weight: 600;
}

.input-box input:focus ~ label,
.input-box input:not(:placeholder-shown) ~ label {
  top: -15px;
}

.remember-forgot {
  font-size: .9em;
  color: #162938;
  font-weight: 600;
  margin: -15px 0 15px;
  padding: 0 5px;
  display: flex;
  justify-content: space-between;
}

.login-btn, .submit-btn {
  color: white;
  font-size: 1em;
  width: 100%;
  height: 45px;
  background-color: #162938;
  border: none;
  border-radius: 3px;
  margin-bottom: 10px;
  transition: .5s;
}

.login-btn:hover, .submit-btn:hover {
  background-color: #10324d;
}

.close-button {
  position: absolute;
  top: 0; right: 0;
  cursor: pointer;
  width: 30px; height: 30px;
  font-size: 2em;
  color: #162938;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 10px;
  transition: .3s;
}

.close-button:hover {
  color: #335e7f;
}
</style>
