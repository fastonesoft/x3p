import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: '/vhome',
            component: () => import( './views/Home.vue')
        },
        {
            path: '/vlogin',
            name: '/vlogin',
            component: () => import( './views/Login.vue')
        },
    ]
})
