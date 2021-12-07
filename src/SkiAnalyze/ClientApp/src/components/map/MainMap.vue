<template>
  <l-map
    ref="map"
    style="height: 500px; z-index: 1"
    :zoom="zoom"
    :center="center">
    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>

    <piste-polyline
      v-for="piste in pistes"
      :key="piste.id"
      :piste="piste" />
    <run-polyline
      v-for="run in runs"
      :key="run.id"
      :selectedRun="selectedRun"
      :run="run" />
  </l-map>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import { LMap, LTileLayer } from 'vue2-leaflet';
import PistePolyline from './PistePolyline.vue';
import RunPolyline from './RunPolyline.vue';

export default {
  props: {
    pistes: Array,
    runs: Array,
    selectedRun: Object,
    center: Array,
    zoom: Number,
    bounds: Object,
  },
  components: {
    LMap,
    LTileLayer,
    PistePolyline,
    RunPolyline,
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
