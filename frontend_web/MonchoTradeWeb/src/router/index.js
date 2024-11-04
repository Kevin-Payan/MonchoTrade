import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import SingupView from '@/views/SingupView.vue'
import LoginView from '@/views/LoginView.vue'
import UploadProductView from '@/views/UploadProductView.vue'
import MyProfileView from '@/views/MyProfileView.vue'
import MyProductsView from '@/views/MyProductsView.vue'
import MyExchangesView from '@/views/MyExchangesView.vue'
import ProposeExchangeView from '@/views/ProposeExchangeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: HomeView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/singup',
      name: 'singup',
      component: SingupView
    },
    {
      path: '/uploadproduct',
      name: 'uploadproduct',
      component: UploadProductView
    },
    {
      path: '/my-profile',
      name: 'my-profile',
      component: MyProfileView
    },
    {
      path: '/my-products',
      name: 'my-products',
      component: MyProductsView
    },
    {
      path: '/my-exchanges',
      name: 'my-exchanges',
      component: MyExchangesView
    },{
      path: '/propose-exchange',
      name: 'ProposeExchange',
      component: ProposeExchangeView
    }
  ]
})

export default router
