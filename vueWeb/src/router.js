import VueRouter from 'vue-router'
import HomeBlog from './components/HomeBlog'
import addBlog from './components/addBlog'
import listBlog from './components/listBlog'
//1 创建路由对象
var router = new VueRouter({
  routes: [{
      path: '/',
      component: HomeBlog,
      name: 'HomeBlog',
    },
    {
      path: '/addBlog',
      component: addBlog,
      name: 'addBlog',
    },
    {
      path: '/listBlog',
      component: listBlog,
      name: 'listBlog',
    }
  ]
})
//2 把路由对象暴露出去
export default router
