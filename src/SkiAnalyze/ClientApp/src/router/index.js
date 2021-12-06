import Vue from 'vue';
import VueRouter from 'vue-router';
import Tracks from '../views/Tracks.vue';
import MapOverview from '../views/MapOverview.vue';
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
    path: '/track/:trackId',
    name: 'Map',
    props: true,
    component: MapOverview,
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
