<template>
  <div>
    <loading-overlay :loading="loading" />
    <div v-if="!loading">
      <v-row>
        <v-col>
          <ski-area-header :skiArea="skiArea" />
        </v-col>
      </v-row>
      <ski-area-detail-cards
        :gondolas="gondolas"
        :pistes="pistes" />
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
              @click="onGondolaSelected(gondola.gondola)"
              :color="getGondolaColor(gondola)"
              :lat-lngs="gondola.latLngs"
              :weight="getGondolaWeight(gondola)" />
            <piste-polyline
              :piste="piste"
              :isSelected="isPisteSelected(piste)"
              v-for="piste in pistes"
              :key="`piste-${piste.id}`" />
          </l-map>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-card>
            <v-card-title>
              {{$t('skiarea.gondolas')}}
            </v-card-title>
            <v-card-text>
              <gondolas
                :gondolas="gondolas"
                :selectedGondola="selectedGondola"
                :displayUsageIndicator="true"
                @showDetails="onShowGondolaDetails"
                @gondolaSelected="onGondolaSelected" />
            </v-card-text>
          </v-card>
        </v-col>
        <v-col>
          <v-card>
            <v-card-title>
              {{$t('skiarea.pistes')}}
            </v-card-title>
            <v-card-text>
              <pistes
                @pisteSelected="onPisteSelected"
                :selectedPiste="selectedPiste"
                :pistes="pistes" />
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </div>
    <v-dialog v-model="showGondolaInfo">
      <v-card>
      <gondola-details :gondola="detailInfoGondola" />
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { latLng, latLngBounds } from 'leaflet';
import SkiAreaHeader from '../components/skiarea/SkiAreaHeader.vue';
import DataService from '../services/DataService';
import GeoHelper from '../services/GeoHelper';
import PistePolyline from '../components/map/PistePolyline.vue';
import Gondolas from '../components/skiarea/Gondolas.vue';
import Pistes from '../components/skiarea/Pistes.vue';
import GondolaDetails from '../components/skiarea/GondolaDetails.vue';
import SkiAreaDetailCards from '../components/skiarea/SkiAreaDetailCards.vue';

export default {
  name: 'TrackDetail',
  props: ['skiAreaId'],
  components: {
    SkiAreaHeader,
    PistePolyline,
    Gondolas,
    Pistes,
    GondolaDetails,
    SkiAreaDetailCards,
  },
  data: () => ({
    loading: true,
    skiArea: null,
    gondolas: [],
    usedGondolas: [],
    pistes: [],
    selectedGondola: null,
    selectedPiste: null,
    showGondolaInfo: false,
    detailInfoGondola: null,
  }),
  methods: {
    async fetchDetail() {
      const response = await DataService.getSkiArea(this.skiAreaId);
      if (response.status === 404) {
        this.$router.replace({ name: '404Resource', params: { resource: 'ski area' } });
      } else {
        this.skiArea = response;
      }
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
    onGondolaSelected(gondola) {
      this.selectedGondola = gondola;
    },
    isPisteSelected(piste) {
      if (!this.selectedPiste) return false;
      return this.selectedPiste.originals.includes(piste);
    },
    onPisteSelected(piste) {
      this.selectedPiste = piste;
    },
    onShowGondolaDetails(gondola) {
      this.detailInfoGondola = gondola;
      this.showGondolaInfo = true;
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
    getGondolaWeight(gondola) {
      if (this.selectedGondola && gondola.id === this.selectedGondola.id) {
        return 4;
      }
      return 2;
    },
    getGondolaColor(gondola) {
      if (this.selectedGondola && gondola.id === this.selectedGondola.id) {
        return 'yellow';
      }
      return 'green';
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
        gondola: g,
        latLngs: g.coordinates.map((c) => [c.latitude, c.longitude]),
      }));
    },
  },
  mounted() {
    this.loadData();
  },
};
</script>
