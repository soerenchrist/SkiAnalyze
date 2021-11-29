<template>
<v-card class="mr-4">
  <v-card-text class="pa-0">
    <l-map
      ref="map"
      style="height: 650px; z-index: 1"
      :zoom="zoom"
      @click="mapClicked"
      :center="center"
      @udpate:center="centerUpdated">
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
import { SET_MAP_BOUNDS, SET_MAP_CENTER } from '../../store/mutations';
import TrackPolylines from '../analysis/TrackPolylines.vue';
import GondolaPolyline from './GondolaPolyline.vue';
import PistePolyline from './PistePolyline.vue';

export default {
  props: {
    gondolas: Array,
    pistes: Array,
    center: Array,
    zoom: Number,
  },
  components: {
    LMap,
    LTileLayer,
    GondolaPolyline,
    PistePolyline,
    TrackPolylines,
  },
  methods: {
    boundsUpdated(bounds) {
      this.$store.commit(SET_MAP_BOUNDS, bounds);
    },
    centerUpdated(center) {
      this.$store.commit(SET_MAP_CENTER, center);
    },
    gondolaSelected(gondola) {
      this.$emit('gondolaClicked', gondola);
    },
    pisteSelected(piste) {
      this.$emit('pisteClicked', piste);
    },
    mapClicked(x) {
      console.log(x.latlng);
    },
  },
  watch: {
    bounds() {
      const sw = latLng(this.bounds.southWest.latitude, this.bounds.southWest.longitude);
      const ne = latLng(this.bounds.northEast.latitude, this.bounds.northEast.longitude);
      const bounds = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(bounds, { padding: [50, 50] });
    },
    isCollapsed() {
      console.log(this.isCollapsed);
      this.$refs.map.mapObject.invalidateSize();
    },
  },
  computed: {
    bounds() {
      return this.$store.getters.bounds;
    },
    isCollapsed() {
      return !this.$store.getters.detailsExpanded;
    },
  },
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
};
</script>
