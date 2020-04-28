import Vue from 'vue'
import Vuex from 'vuex'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App.vue'
import VueRouter from 'vue-router'
import router from './router.js'
import store from './store/index.js'
//import VueResource from 'vue-resource'
import Moment from 'moment'
import { post, get } from "./request/http";
// 安装 ElementUI（ui）
Vue.use(ElementUI)

// 安装 路由（url）
Vue.use(VueRouter)

import { registryToast } from './plugins/ToastMessage' // message 提示消息插件
Vue.use(registryToast)
// 绑定 vue-resource（ajax）
//Vue.use(VueResource)


Vue.use(Vuex)

//定义全局变量
Vue.prototype.$post = post;
Vue.prototype.$get = get;
Vue.prototype.$store = store;

// 绑定 moment 进行时间格式化 ✔
Vue.prototype.$moment = Moment;//赋值使用

new Vue({
  el: '#app',
  render: h => h(App),
  // 挂在路由对象到 VM 实例上
  router
})
