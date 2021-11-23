<template>
  <l-map style="height: 500px" :zoom="zoom" :center="center">
    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
    <l-polyline
      v-for="polyline in polylines"
      :key="polyline.id"
      :lat-lngs="polyline.latLngs"
      :color="polyline.color" />
  </l-map>
</template>

<script>
import { LMap, LTileLayer } from 'vue2-leaflet';
import DataService from '../services/DataService';

export default {
  name: 'Home',
  components: {
    LMap,
    LTileLayer,
  },
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
    zoom: 15,
    gondolas: [],
    pistes: [],
    center: [46.966709, 11.007390],
    polylines: [],
  }),
  methods: {
    async fetchGondolas() {
      this.gondolas = await DataService.getGondolas();
      this.gondolas.forEach((x) => {
        const latLngs = this.getLatLngs(x);
        this.polylines.push({
          id: x.osmId,
          color: 'green',
          latLngs,
        });
      });
    },
    async fetchPistes() {
      this.pistes = await DataService.getPistes();
      this.pistes.forEach((x) => {
        const latLngs = this.getLatLngs(x);
        this.polylines.push({
          id: x.osmId,
          color: this.getColor(x),
          latLngs,
        });
      });
    },
    getColor(piste) {
      if (piste.difficulty <= 1) return 'blue';
      if (piste.difficulty === 2) return 'red';
      return 'black';
    },
    getLatLngs(gondola) {
      const latLngs = [];
      gondola.coordinates.forEach((x) => {
        latLngs.push([x.latitude, x.longitude]);
      });
      return latLngs;
    },
  },
  mounted() {
    this.fetchGondolas();
    this.fetchPistes();
  },
};
</script>
