<template>
  <v-card
    class="clickable"
    @click="onClick">
    <v-card-title>
      Map
    </v-card-title>
    <v-card-text class="pa-0">
      <l-map
        ref="map"
        :zoom="zoom"
        :options="options"
        style="height: 250px; z-index: 1"
        :center="center">
        <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
        <l-polyline
          v-if="hasPreview"
          :lat-lngs="latLngs"
          color="blue" />
      </l-map>
    </v-card-text>
  </v-card>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';

export default {
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    center: [49, 9],
    zoom: 12,
    options: {
      dragging: false,
      zoomControl: false,
      scrollWheelZoom: false,
    },
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
  methods: {
    onClick() {
      this.$router.push('map');
    },
  },
  computed: {
    preview() {
      return this.$store.getters.preview;
    },
    hasPreview() {
      return this.preview !== null;
    },
    latLngs() {
      const coords = [];
      this.preview.coordinates.forEach((x) => {
        coords.push([x.latitude, x.longitude]);
      });
      return coords;
    },
  },
  watch: {
    preview() {
      if (!this.preview) return;
      const swLat = this.preview.bounds.southWest.latitude;
      const swLon = this.preview.bounds.southWest.longitude;
      const neLat = this.preview.bounds.northEast.latitude;
      const neLon = this.preview.bounds.northEast.longitude;
      const sw = latLng(swLat, swLon);
      const ne = latLng(neLat, neLon);
      const bounds = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(bounds, { padding: [-20, -20] });
    },
  },
};
</script>

<style scoped>
.clickable {
  cursor: pointer;
}
</style>
