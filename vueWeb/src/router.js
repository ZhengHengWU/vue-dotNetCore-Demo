import VueRouter from 'vue-router'
import addBlog from './components/addBlog'
import listBlog from './components/listBlog'
import login from './components/login'
import cookie from './util/cookie'
//1 创建路由对象
var router = new VueRouter({
  routes: [
  {
    path: '/addBlog',
    component: addBlog,
    name: 'addBlog'
  },
  {
    path: '/listBlog',
    component: listBlog,
    name: 'listBlog'
  },
  {
    path: '/',
    component: login,
    name: 'login'
  }
  ]
})
//2 把路由对象暴露出去
export default router
