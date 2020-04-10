import Vue from 'vue'
import Router from 'vue-router'

import Login from '@/views/Login'
import Home from '@/views/Home'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    { path: '/Login', name: 'Login', component: Login },
    { path: '/Home', name: 'Home', component: Home },
    { path: '/', redirect: { name: 'Login' } },
  ]
})
