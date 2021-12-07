<template>
  <l-map
    ref="map"
    style="height: 500px; z-index: 1"
    :zoom="zoom"
    :center="center">
    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>

    <run-polyline
      v-for="run in runs"
      :key="run.id"
      :selectedRun="selectedRun"
      :run="run" />

    <div v-if="showPistes">
      <piste-polyline
        v-for="piste in pistes"
        :key="piste.id"
        :piste="piste" />
    </div>

    <gondola-polyline :gondola="gondola" />

    <l-control
        :position="'bottomleft'">
        <v-sheet
          color="white"
          elevation="3"
          rounded="md"
          class="controlSheet" >
          <v-checkbox label="Show pistes" v-model="showPistes" class="ma-0" />
        </v-sheet>
    </l-control>
  </l-map>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import { LMap, LTileLayer } from 'vue2-leaflet';
import PistePolyline from './PistePolyline.vue';
import GondolaPolyline from './GondolaPolyline.vue';
import RunPolyline from './RunPolyline.vue';

export default {
  props: {
    pistes: Array,
    gondola: Object,
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
    GondolaPolyline,
  },
  watch: {
    bounds() {
      if (!this.bounds) return;
      this.fitBounds(this.bounds);
    },
  },
  methods: {
    resize() {
      // weird behavior without timeout
      setTimeout(() => {
        this.$refs.map.mapObject.invalidateSize(true);
        if (this.bounds) {
          this.fitBounds(this.bounds);
        }
      }, 500);
    },
    fitBounds(bounds) {
      const sw = latLng(bounds.southWest.latitude, bounds.southWest.longitude);
      const ne = latLng(bounds.northEast.latitude, bounds.northEast.longitude);
      const b = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(b, { padding: [50, 50] });
    },
  },
  data: () => ({
    showPistes: false,
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
};
</script>

<style>
.controlSheet {
  padding: 1em 1em 0 1em
}
</style>
