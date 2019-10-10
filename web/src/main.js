import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import DevArticle from './components/dev-article.vue'
import './plugins/iview.js'

// Vue自带的
Vue.config.productionTip = false;

// 自定义模板
Vue.component('dev-article', DevArticle);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
