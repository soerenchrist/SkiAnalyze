<template>
  <v-card class="ma-4">
    <v-card-title>
      Ski areas
    </v-card-title>
    <v-card-text class="pa-0">
      <l-map
        ref="map"
        @ready="mapReady"
        style="height: 650px; z-index: 1"
        :zoom.sync="zoom"
        :center="center"
        :bounds.sync="bounds"
        :minZoom="11">
        <l-tile-layer
          :url="url"
          :attribution="attribution" />
        <l-marker
          v-for="marker in markers"
          :key="marker.id"
          :lat-lng="marker.latLng">
          <l-tooltip :options="{ permanent: true, interactive: true}">
            {{marker.name}}
          </l-tooltip>
          <l-popup>
            {{marker.name}}
          </l-popup>
        </l-marker>
        <l-polyline
          v-for="polyline in polylines"
          :key="polyline.id"
          :lat-lngs="polyline.latLngs"
          :fill="true"
          fillColor="orange"
          color="orange">
          <l-popup>
            {{polyline.name}}
          </l-popup>
        </l-polyline>
      </l-map>
    </v-card-text>
  </v-card>
</template>

<script>
import DataService from '../../services/DataService';

export default {
  data: () => ({
    zoom: 11,
    center: [47, 12],
    bounds: null,
    areas: [],
    details: [],
    url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    attribution:
        '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
  }),
  watch: {
    bounds() {
      this.fetchSkiAreas(this.bounds);
    },
  },
  computed: {
    markers() {
      return this.areas.map((a) => ({
        latLng: [a.centerLatitude, a.centerLongitude],
        name: a.name,
        id: `marker-${a.id}`,
      }));
    },
    polylines() {
      return this.details.map((d) => ({
        id: `poly-${d.id}`,
        name: d.name,
        latLngs: d.nodes.map((node) => [node.latitude, node.longitude]),
      }));
    },
  },
  methods: {
    async fetchSkiAreas(bounds) {
      const b = {
        northEast: {
          latitude: bounds._northEast.lat,
          longitude: bounds._northEast.lng,
        },
        southWest: {
          latitude: bounds._southWest.lat,
          longitude: bounds._southWest.lng,
        },
      };
      this.areas = await DataService.getSkiAreas(b);
      if (this.zoom >= 12 && this.areas) {
        await this.fetchDetails();
      } else {
        this.details = [];
      }
    },
    async fetchDetails() {
      const promises = [];
      this.areas.forEach((area) => {
        promises.push(DataService.getSkiArea(area.id));
      });

      this.details = await Promise.all(promises);
    },
    mapReady() {
      const bounds = this.$refs.map.mapObject.getBounds();
      this.fetchSkiAreas(bounds);
    },
  },
};
</script>
