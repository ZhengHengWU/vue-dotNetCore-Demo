import Vue from 'vue';
import Vuex from 'vuex';
import VuexPersistence from 'vuex-persist';
Vue.use(Vuex);
const vuexLocal = new VuexPersistence({
  storage: window.localStorage,
  reducer: state => ({
    token: state.token, // 这个就是存入localStorage的值
    WXTempKey: state.WXTempKey
  })
})
const store = new Vuex.Store({
  state: {
    token: "",
    isLogin: false,
    uid: 0,
    name: ''
  },
  mutations: {
    setToken(state, token) {
      state.token = token
    },
    setLogin(state, flag) {
      state.isLogin = flag
    }
  },
  actions: {},
  plugins: [vuexLocal.plugin]
});

export default store;