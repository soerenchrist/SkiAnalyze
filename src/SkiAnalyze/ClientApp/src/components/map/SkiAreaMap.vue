<template>
  <v-card>
    <v-card-title>
      {{$t('menu.skiareas')}}
    </v-card-title>
    <v-card-text class="pa-0">
      <l-map
        ref="map"
        @ready="mapReady"
        style="height: 650px; z-index: 1"
        :zoom.sync="zoom"
        :center="center"
        :bounds.sync="bounds">
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
            <span class="font-weight-bold d-block mb-2">{{marker.name}}</span>
            <v-btn small color="primary" @click="showDetails(marker)">
              {{$t('skiarea.showDetails')}}
            </v-btn>
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
            <span class="font-weight-bold d-block mb-2">{{polyline.name}}</span>
            <v-btn small color="primary" @click="showDetails(polyline)">Show detail</v-btn>
          </l-popup>
        </l-polyline>
      </l-map>
    </v-card-text>
  </v-card>
</template>

<script>
import GeoHelper from '../../services/GeoHelper';

export default {
  props: {
    areas: Array,
    details: Array,
  },
  data: () => ({
    zoom: 11,
    center: [47, 12],
    bounds: null,
    url: GeoHelper.url,
    attribution: GeoHelper.attribution,
  }),
  watch: {
    bounds() {
      // this.fetchSkiAreas(this.bounds);
      this.emitBounds(this.bounds);
    },
  },
  computed: {
    markers() {
      return this.areas.map((a) => ({
        latLng: [a.centerLatitude, a.centerLongitude],
        name: a.name,
        area: a,
        id: `marker-${a.id}`,
      }));
    },
    polylines() {
      return this.details.map((d) => ({
        id: `poly-${d.id}`,
        name: d.name,
        area: d,
        latLngs: d.nodes.map((node) => [node.latitude, node.longitude]),
      }));
    },
  },
  methods: {
    showDetails(marker) {
      this.$emit('showDetails', marker.area);
    },
    emitBounds(bounds) {
      this.$emit('boundsChanged', bounds, this.zoom);
    },
    fitBounds(bounds) {
      this.$refs.map.fitBounds(bounds, { maxZoom: 12 });
    },
    mapReady() {
      const bounds = this.$refs.map.mapObject.getBounds();
      this.emitBounds(bounds);
      // this.fetchSkiAreas(bounds);
    },
  },
};
</script>
