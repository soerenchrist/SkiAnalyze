<template>
<v-card>
  <v-card-text class="mt-4 mr-4 pa-0">
    <l-map
      style="height: 650px; z-index: 1"
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
        v-for="piste in filteredPistes"
        :key="piste.id"
        :piste="piste"
        @onclick="pisteSelected" />
    </l-map>
  </v-card-text>
</v-card>
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
  computed: {
    filteredPistes() {
      return this.$store.getters.filteredPistes;
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
