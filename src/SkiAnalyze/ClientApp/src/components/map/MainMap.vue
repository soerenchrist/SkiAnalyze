<template>
<v-card>
  <v-card-text class="pa-0">
    <l-map
      ref="map"
      style="height: 500px; z-index: 1"
      :zoom="zoom"
      :center="center">
      <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
      <gondola-polyline
        v-for="gondola in gondolas"
        :key="gondola.id"
        :gondola="gondola"
        @onclick="gondolaSelected" />

      <piste-polyline
        v-for="piste in pistes"
        :key="piste.id"
        :piste="piste"
        @onclick="pisteSelected" />
        <track-polylines />
    </l-map>
  </v-card-text>
</v-card>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import { LMap, LTileLayer } from 'vue2-leaflet';
import TrackPolylines from '../analysis/TrackPolylines.vue';
import GondolaPolyline from './GondolaPolyline.vue';
import PistePolyline from './PistePolyline.vue';

export default {
  props: {
    gondolas: Array,
    pistes: Array,
    center: Array,
    zoom: Number,
    bounds: Object,
  },
  components: {
    LMap,
    LTileLayer,
    GondolaPolyline,
    PistePolyline,
    TrackPolylines,
  },
  watch: {
    bounds() {
      if (!this.bounds) return;
      const sw = latLng(this.bounds.southWest.latitude, this.bounds.southWest.longitude);
      const ne = latLng(this.bounds.northEast.latitude, this.bounds.northEast.longitude);
      const bounds = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(bounds, { padding: [50, 50] });
    },
  },
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
};
</script>
