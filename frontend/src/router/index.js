import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home.vue';
import MapOverview from '../views/MapOverview.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/map',
    name: 'Map',
    component: MapOverview,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
