import {
  LMap,
  LTileLayer,
  LMarker,
  LPolyline,
  LPopup,
  LTooltip,
  LIcon,
  LControl,
} from 'vue2-leaflet';
import { ChartPlugin, AccumulationChartPlugin } from '@syncfusion/ej2-vue-charts';
import LoadingOverlay from './components/common/LoadingOverlay.vue';

export default (vue) => {
  vue.component('loading-overlay', LoadingOverlay);
  vue.use(ChartPlugin);
  vue.use(AccumulationChartPlugin);
  vue.component('l-map', LMap);
  vue.component('l-popup', LPopup);
  vue.component('l-tile-layer', LTileLayer);
  vue.component('l-marker', LMarker);
  vue.component('l-icon', LIcon);
  vue.component('l-tooltip', LTooltip);
  vue.component('l-popup', LPopup);
  vue.component('l-polyline', LPolyline);
  vue.component('l-control', LControl);
};
