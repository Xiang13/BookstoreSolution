<template>
  <div v-if="uiStore.showLogin" class="wrapper" @click.self="uiStore.closeLogin">

    <div v-if="uiStore.loadingMap.auth" class="form-box loading-box" @click.stop>
      <h3 style="text-align: center;">載入中...</h3>
      <div class="d-flex justify-content-center align-items-center" style="height: 150px;">
        <div class="spinner-border" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>
    <!-- 登入視窗 -->
    <div v-else-if="!uiStore.isRegistering" class="form-box login" @click.stop>
      <form @submit.prevent="login">
        <span class="close-button" @click="uiStore.closeLogin">&times;</span>
        <h2>登入</h2>
        <div class="input-box">
          <input v-model="email" type="email" placeholder=" " required />
          <label>Email</label>
        </div>
        <div class="input-box">
          <input v-model="password" type="password" placeholder=" " required />
          <label>Password</label>
        </div>
        <div class="remember-forgot">
          <label><input type="checkbox" />記住我</label>
          <a href="#">忘記密碼?</a>
        </div>
        <button type="submit" class="login-btn">登入</button>
        <div class="login-register">
          <p>沒有帳戶?<a href="#" class="register-link" @click.prevent="register">註冊</a></p>
        </div>
      </form>
    </div>

    <!-- 註冊視窗 -->
    <div v-else class="form-box register" @click.stop>
      <span class="close-button" @click="uiStore.closeLogin">&times;</span>
      <h2>註冊</h2>
      <form @submit.prevent="confirmRegister">
        <div class="input-box">
          <input v-model="username" type="text" placeholder="" required />
          <label>User Name</label>
        </div>
        <div class="input-box">
          <input v-model="email" type="email" placeholder="" required />
          <label>Email</label>
        </div>
        <div class="input-box">
          <input v-model="password" type="password" placeholder="" required />
          <label>Password</label>
        </div>
        <div class="input-box">
          <input v-model="confirmPassword" type="password" placeholder="" required />
          <label>Confirm Password</label>
        </div>
        <div class="remember-forgot">
          <label><input type="checkbox" /> 我已閱讀並同意 會員條款</label>
        </div>
        <div class="submit-box">
          <button type="submit" class="submit-btn">確認</button>
          <button type="button" class="submit-btn" @click="cancel">取消</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'

// store
import { useUIStore } from '@/stores/uiStore'
// store 實例
const uiStore = useUIStore()

const username = ref('')
const password = ref('')
const email = ref('')
const confirmPassword = ref('')

// 當 login modal 開啟時，重置狀態
watch(
  () => uiStore.showLogin,
  async (newVal) => {
    if (newVal) {
      uiStore.isRegistering = false
      resetForm()
      uiStore.loadingMap.auth = true
      await new Promise(resolve => setTimeout(resolve, 1000))
      uiStore.loadingMap.auth = false
    }
  }
)

const login = () => {
  resetForm()
  if (email.value && password.value) {
    console.log('登入中：', email.value, password.value)
    // 模擬登入成功後關閉 modal
  } else {
    alert('請輸入帳號與密碼')
  }
}

const register = async () => {
  uiStore.loadingMap.auth = true
  uiStore.isRegistering = true
  await uiStore.delay(1000)
  uiStore.loadingMap.auth = false
  resetForm()
}

const cancel = async () => {
  uiStore.loadingMap.auth = true
  await uiStore.delay(1000)
  uiStore.isRegistering = false
  uiStore.loadingMap.auth = false
  resetForm()
}

const confirmRegister = () => {
  console.log('註冊資料：', username.value, email.value, password.value, confirmPassword.value)
  // 可以加檢查條件
  if (password.value !== confirmPassword.value) {
    alert('兩次輸入的密碼不一致')
    return
  }
  // 模擬註冊成功
  uiStore.closeLogin()
}

const resetForm = () => {
  username.value = ''
  password.value = ''
  email.value = ''
  confirmPassword.value = ''
}
</script>

<style scoped>
/* 從 home.css 抽取來的樣式 */
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
