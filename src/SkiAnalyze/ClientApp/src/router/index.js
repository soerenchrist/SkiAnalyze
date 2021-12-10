import Vue from 'vue';
import VueRouter from 'vue-router';
import Tracks from '../views/Tracks.vue';
import TrackDetail from '../views/TrackDetail.vue';
import SkiAreas from '../views/SkiAreas.vue';
import Dashboard from '../views/Dashboard.vue';
import SkiAreaDetail from '../views/SkiAreaDetail.vue';

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
    path: '/skiareas',
    name: 'SkiAreas',
    component: SkiAreas,
  },
  {
    path: '/skiareas/:skiAreaId',
    name: 'SkiAreaDetail',
    component: SkiAreaDetail,
    props: true,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
