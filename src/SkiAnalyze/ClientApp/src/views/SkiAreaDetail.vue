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
            @ready="onMapReady"
            style="height: 500px; z-index:1"
            :zoom="13"
            :center="[46,10]">
            <l-tile-layer :url="url" :attribution="attribution" />
            <l-polyline
              :lat-lngs="polyline"
              color="orange"
              :weight="1" />

            <l-polyline
              v-for="gondola in gondolaPolylines"
              :key="`gondola-${gondola.id}`"
              color="green"
              :lat-lngs="gondola.latLngs"
              :weight="2" />
            <piste-polyline
              :piste="piste"
              v-for="piste in pistes"
              :key="`piste-${piste.id}`" />
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
import PistePolyline from '../components/map/PistePolyline.vue';

export default {
  name: 'TrackDetail',
  props: {
    skiAreaId: String,
  },
  components: {
    SkiAreaHeader,
    PistePolyline,
  },
  data: () => ({
    loading: true,
    skiArea: null,
    gondolas: [],
    pistes: [],
  }),
  methods: {
    async fetchDetail() {
      this.skiArea = await DataService.getSkiArea(this.skiAreaId);
    },
    onMapReady() {
      if (this.skiArea) {
        this.fitBounds(this.skiArea.nodes);
      } else {
        setTimeout(() => {
          this.onMapReady();
        }, 200);
      }
    },
    async fetchGondolas() {
      this.gondolas = await DataService.getGondolasForSkiArea(this.skiAreaId);
    },
    fitBounds(nodes) {
      const bounds = GeoHelper.getBounds(nodes);
      const sw = latLng(bounds.southWest.latitude, bounds.southWest.longitude);
      const ne = latLng(bounds.northEast.latitude, bounds.northEast.longitude);
      const b = latLngBounds(sw, ne);
      this.$refs.map.fitBounds(b, { padding: [50, 50] });
    },
    async fetchPistes() {
      this.pistes = await DataService.getPistesForSkiArea(this.skiAreaId);
    },
    getPisteColor(piste) {
      switch (piste.difficulty) {
        default: return 'blue';
      }
    },
    async loadData() {
      const promises = [];
      this.loading = true;
      promises.push(this.fetchDetail());
      promises.push(this.fetchGondolas());
      promises.push(this.fetchPistes());

      await Promise.all(promises);
      this.loading = false;
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
    gondolaPolylines() {
      return this.gondolas.map((g) => ({
        name: g.name,
        id: g.id,
        latLngs: g.coordinates.map((c) => [c.latitude, c.longitude]),
      }));
    },
  },
  mounted() {
    this.loadData();
  },
};
</script>
