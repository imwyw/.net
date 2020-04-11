import Vue from 'vue'
import Router from 'vue-router'

import Login from '@/views/Login'
import Home from '@/views/Home'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: [
    { path: '/Login', name: 'Login', component: Login },
    {
      path: '/Home', name: 'Home', component: Home,
      meta: { requireAuth: true }// requireAuth 添加该字段，表示进入这个路由是需要登录的
    },
    { path: '/', redirect: { name: 'Home' } },// 默认跳转主页
  ]
});

/*
全局前置守卫，通过 token 进行登录拦截
https://router.vuejs.org/zh/guide/advanced/navigation-guards.html#%E5%85%A8%E5%B1%80%E5%89%8D%E7%BD%AE%E5%AE%88%E5%8D%AB
*/
router.beforeEach((to, from, next) => {
  debugger;
  // 判断该路由是否需要登录权限
  if (to.meta.requireAuth) {
    // 没有 token ，则跳转至 登录页面
    if (!localStorage.getItem('Token')) {
      next({ name: 'Login' });
    } else {
      next();
    }
  } else {
    next();
  }
})

// router 对象作为默认模块导出
export default router;
