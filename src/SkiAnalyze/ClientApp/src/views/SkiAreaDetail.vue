<template>
  <div>
    <loading-overlay :loading="loading" />
    <div v-if="!loading">
      <v-row>
        <v-col>
          <ski-area-header :skiArea="skiArea" />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <l-map
            ref="map"
            style="height: 500px; z-index:1"
            :zoom="13"
            :center="[46,10]">
            <l-tile-layer :url="url" :attribution="attribution" />
            <l-polyline
              :lat-lngs="polyline"
              color="orange"
              :weight="1" />
          </l-map>
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import SkiAreaHeader from '../components/skiarea/SkiAreaHeader.vue';
import DataService from '../services/DataService';
import GeoHelper from '../services/GeoHelper';

export default {
  name: 'TrackDetail',
  props: {
    skiAreaId: String,
  },
  components: {
    SkiAreaHeader,
  },
  data: () => ({
    loading: true,
    skiArea: null,
  }),
  methods: {
    async fetchDetail() {
      this.loading = true;
      this.skiArea = await DataService.getSkiArea(this.skiAreaId);
      setTimeout(() => {
        this.fitBounds(this.skiArea.nodes);
      }, 500);
      this.loading = false;
    },
    fitBounds(nodes) {
      const bounds = GeoHelper.getBounds(nodes);
      const sw = latLng(bounds.southWest.latitude, bounds.southWest.longitude);
      const ne = latLng(bounds.northEast.latitude, bounds.northEast.longitude);
      const b = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(b, { padding: [50, 50] });
    },
  },
  computed: {
    attribution() {
      return GeoHelper.attribution;
    },
    url() {
      return GeoHelper.url;
    },
    polyline() {
      return this.skiArea.nodes.map((node) => [node.latitude, node.longitude]);
    },
  },
  mounted() {
    this.fetchDetail();
  },
};
</script>
