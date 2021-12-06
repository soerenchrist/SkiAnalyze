import Vue from 'vue';
import VueRouter from 'vue-router';
import Tracks from '../views/Tracks.vue';
import TrackDetail from '../views/TrackDetail.vue';
import TrackStats from '../views/TrackStats.vue';
import SkiAreas from '../views/SkiAreas.vue';
import Dashboard from '../views/Dashboard.vue';

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
    name: 'TrackDetail',
    props: true,
    component: TrackDetail,
  },
  {
    path: '/stats/:trackId',
    name: 'TrackStats',
    props: true,
    component: TrackStats,
  },
  {
    path: '/skiareas',
    name: 'SkiAreas',
    component: SkiAreas,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
