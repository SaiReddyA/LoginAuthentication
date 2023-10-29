import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue';
import EmployeeView from '../views/EmpTask/EmployeeView.vue';
import CreateGender from '../views/EmpTask/CreateGender.vue';
import Edit from '../views/EmpTask/Edit.vue'
import Signup from '../views/Login/Signup.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/signin',
      name: 'signin',
      component: Signup
    }
  ]
})

export default router
