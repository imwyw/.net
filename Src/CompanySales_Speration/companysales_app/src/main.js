// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'

// 导入 ElementUI 组件
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
Vue.use(ElementUI);

// 导入 axios 组件
import axios from 'axios'
import VueAxios from 'vue-axios'
Vue.use(VueAxios, axios);

/* vue-cookies 工具
https://www.npmjs.com/package/vue-cookies
*/
import VueCookies from 'vue-cookies'
Vue.use(VueCookies);

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  render: h => h(App),
  created() {
    // 设置全局的 webapi url 基路径
    this.axios.defaults.baseURL = 'http://localhost:5000/api';
  }
  // components: { App },
  // template: '<App/>'
})
