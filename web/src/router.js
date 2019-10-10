import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: '*',
            component: () => import( './views/Home.vue')
        },
        {
            path: '/vhome',
            name: '/vhome',
            component: () => import( './views/Home.vue')
        },
        {
            path: '/vlogin/infor',
            name: '/vlogin',
            component: () => import( './views/Login.vue')
        },
    ]
})
