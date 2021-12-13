import Vue from 'vue';
import VueRouter from 'vue-router';
import Tracks from '../views/Tracks.vue';
import TrackDetail from '../views/TrackDetail.vue';
import SkiAreas from '../views/SkiAreas.vue';
import Dashboard from '../views/Dashboard.vue';
import SkiAreaDetail from '../views/SkiAreaDetail.vue';
import NotFound from '../views/NotFound.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Dashboard',
    component: Dashboard,
  },
  {
    path: '/tracks',
    name: 'Tracks',
    component: Tracks,
  },
  {
    path: '/tracks/:trackId',
    name: 'Track',
    props: true,
    component: TrackDetail,
  },
  {
    path: '/skiareas',
    name: 'SkiAreas',
    component: SkiAreas,
  },
  {
    path: '/skiareas/:skiAreaId',
    name: 'SkiArea',
    component: SkiAreaDetail,
    props: true,
  },
  {
    path: '/:catchAll(.*)',
    name: 'NotFound',
    component: NotFound,
  },
  {
    path: '/404/:resource',
    name: '404Resource',
    component: NotFound,
    props: true,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
