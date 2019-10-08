import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from './libs/axios'
import devArticle from './components/dev-article.vue'
import './plugins/iview.js'

// 跨域
Vue.prototype.$ = axios.ajax;
Vue.prototype.$.all = axios.all;
Vue.prototype.$.spread = axios.spread;

// Vue自带的
Vue.config.productionTip = false;

// 自定义模板
Vue.component('dev-article', devArticle);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
