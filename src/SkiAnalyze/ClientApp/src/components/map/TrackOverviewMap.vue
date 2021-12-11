<template>
  <l-map
    ref="map"
    style="height: 500px; z-index: 1"
    :zoom="zoom"
    :maxZoom="14"
    :center="center"
    @update:bounds="boundsUpdated">
    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
    <l-control
        :position="'bottomleft'">
        <v-sheet
          color="white"
          elevation="3"
          rounded="md"
          class="controlSheet" >
          <v-checkbox :label="$t('tracks.syncMap')" v-model="internalSync" class="ma-0" />
        </v-sheet>
    </l-control>
  </l-map>
</template>

<script>
// eslint-disable-next-line no-unused-vars
import { markerClusterGroup } from 'leaflet.markercluster';
import { latLng, marker } from 'leaflet';
import GeoHelper from '../../services/GeoHelper';

export default {
  model: {
    prop: 'sync',
    event: 'syncChanged',
  },
  props: {
    tracks: Array,
    sync: Boolean,
  },
  data: () => ({
    zoom: 8,
    internalSync: true,
    center: [47, 12],
  }),
  methods: {
    buildMarkerGroup() {
      // eslint-disable-next-line no-undef
      const markers = L.markerClusterGroup();
      this.tracks.forEach((track) => {
        if (track.skiArea) {
          markers.addLayer(
            marker(latLng(track.skiArea.centerLatitude, track.skiArea.centerLongitude)),
          );
        }
      });
      this.$refs.map.mapObject.addLayer(markers);
    },
    boundsUpdated(bounds) {
      this.$emit('boundsUpdated', bounds);
    },
  },
  mounted() {
    this.buildMarkerGroup();
  },
  watch: {
    sync() {
      if (this.internalSync !== this.sync) {
        this.internalSync = this.sync;
      }
    },
    internalSync() {
      this.$emit('syncChanged', this.internalSync);
    },
  },
  computed: {
    attribution() {
      return GeoHelper.attribution;
    },
    url() {
      return GeoHelper.url;
    },
  },
};
</script>

<style>
.controlSheet {
  padding: 1em 1em 0 1em
}
</style>
