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
        style="height: 200px; z-index: 1"
        :center="center">
        <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
      </l-map>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    center: [49, 9],
    zoom: 12,
    options: {
      dragging: false,
      zoomControl: false,
    },
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
  methods: {
    onClick() {
      this.$router.push('map');
    },
  },
};
</script>

<style scoped>
.clickable {
  cursor: pointer;
}
</style>
