<template>
  <l-map
    style="height: 500px"
    :zoom="zoom"
    :center="center"
    @update:bounds="boundsUpdated">
    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
    <gondola-polyline
      v-for="gondola in gondolas"
      :key="gondola.id"
      :gondola="gondola"
      @onclick="gondolaSelected"
      color="green" />

    <piste-polyline
      v-for="piste in pistes"
      :key="piste.id"
      :piste="piste"
      @onclick="pisteSelected" />
  </l-map>
</template>

<script>
import { LMap, LTileLayer } from 'vue2-leaflet';
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
  },
  methods: {
    boundsUpdated(bounds) {
      this.bounds = bounds;
    },
    gondolaSelected(gondola) {
      this.$emit('gondolaClicked', gondola);
    },
    pisteSelected(piste) {
      this.$emit('pisteClicked', piste);
    },
  },
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
    bounsd: null,
  }),
};
</script>
