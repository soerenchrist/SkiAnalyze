/* eslint-disable global-require */
import Vue from 'vue';
import {
  LMap,
  LTileLayer,
  LMarker,
  LPolyline,
  LPopup,
  LTooltip,
} from 'vue2-leaflet';
import { ChartPlugin, AccumulationChartPlugin } from '@syncfusion/ej2-vue-charts';
import 'leaflet/dist/leaflet.css';
import { Icon } from 'leaflet';
import App from './App.vue';
import router from './router';
import store from './store';
import vuetify from './plugins/vuetify';

// eslint-disable-next-line no-underscore-dangle
delete Icon.Default.prototype._getIconUrl;
Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});

Vue.config.productionTip = false;

Vue.use(ChartPlugin);
Vue.use(AccumulationChartPlugin);
Vue.component('l-map', LMap);
Vue.component('l-popup', LPopup);
Vue.component('l-tile-layer', LTileLayer);
Vue.component('l-marker', LMarker);
Vue.component('l-tootlip', LTooltip);
Vue.component('l-popup', LPopup);
Vue.component('l-polyline', LPolyline);

new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount('#app');