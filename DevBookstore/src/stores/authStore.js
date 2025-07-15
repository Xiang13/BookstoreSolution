// 管理使用者登入狀態的 Pinia store
import { useUIStore } from '@/stores/uiStore'
import { useBookStore } from '@/stores/bookStore'
import { defineStore } from 'pinia'
import { delay } from '@/utils/delay'
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'


export const useAuthStore = defineStore('auth', () => {
  const uiStore = useUIStore()
  const bookStore = useBookStore()
  const isLoggedIn = ref(false)
  const userProfile = ref({
    userId: null,
    displayName: '',
    email: '',
    roles: []
  });
  const isRegistering = ref(false)
  const router = useRouter()
  // 勾選狀態
  const rememberMe = ref(false)
  const agreeTerms = ref(false)
  // 錯誤訊息
  const errors = ref({})

  // 登入資料
  const loginData = ref({ email: '', password: '' })
  // 註冊資料
  const registerData = ref({ userName: '', email: '', password: '', confirmPassword: '' })
  // 登入
  const isInitialized = ref(false)

  // 登入狀態
  const handleLogin = async () => {
    if (!validateLogin()) return
    try {    
      uiStore.loadingMap.auth = true
      
      const res = await axios.post('/Auth/login', {
        Email: loginData.value.email,
        Password: loginData.value.password,
      }, {
        withCredentials: true
      });
      await delay(500)
      if (res.status === 200) {
        const { message } = res.data;
        console.log("message", message)
        // 登入成功後馬上初始化狀態
        await checkAuthStatus()
        bookStore.resetBookPageState()
      } else{
      }
      if (rememberMe.value) {
        localStorage.setItem('rememberedEmail', loginData.value.email)
      } else {
        localStorage.removeItem('rememberedEmail')
      }
      resetForms()
      uiStore.closeLogin()
      router.push('/')

    } catch (err) {
      console.error('Login error:', err)
      errors.value.login = err.response?.data?.message || '登入失敗，請再試一次'
    } finally {
      uiStore.loadingMap.auth = false
    }
  }

  // 判斷登入狀態
  const checkAuthStatus = async () => {
    try {
      const res = await axios.get('/Member/profile', {
        withCredentials: true
      })
      await delay(500)
      const user = res.data
      userProfile.value = {
        displayName: user.displayName,
        userId: user.userId,
        email: user.email,
        roles: user.roles,
      }
      isLoggedIn.value = true      
      
    } catch (err) {
      if (err.response && err.response.status === 401) {
        // 使用者未登入，不顯示錯誤
        console.log('尚未登入，略過 401 錯誤');
      } else {
        console.error('發生其他錯誤', err);        
      }
      isLoggedIn.value = false
      console.log("非登入狀態")
    } finally {
        isInitialized.value = true;
    }
  }

  // 登出並清除 localStorage
  const handleLoginout = async () => {
    const confirmLogout = window.confirm("你確定要登出嗎？");

    if (!confirmLogout) {
      return; // 使用者按取消
    }
    uiStore.loadingMap.auth = true
    try{
      await delay(500)
      await axios.post('/auth/logout', null, { withCredentials: true });
      isLoggedIn.value = false
      userProfile.value = {
        displayName: '',
        userId: null,
        email: '',
        roles: []
      }
      bookStore.resetBookPageState()
      console.log("登出成功 ")
      bookStore.resetBookPageState()
      router.push('/')
    } catch (err) {
      alert("登出失敗");
      console.error(err);
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

  // 清空所有表單內容
  const resetForms = () => {
  loginData.value.email = '';
  loginData.value.password = '';
  registerData.value.userName = '';
  registerData.value.email = '';
  registerData.value.password = '';
  registerData.value.confirmPassword = '';
  agreeTerms.value = false
}  

  const hasRole = (role) => {
    return roles.value.includes(role)
  }

  return {
    isLoggedIn,
    errors,
    rememberMe,
    agreeTerms,
    loginData,
    registerData,
    isRegistering,
    userProfile,
    isInitialized,
    checkAuthStatus,
    handleLogin,
    handleLoginout,
    handleRegister,
    hasRole,
    resetForms,
  }
})
