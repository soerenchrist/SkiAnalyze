<template>
  <div>
    <v-overlay :value="loading">
      <v-progress-circular
        indeterminate
        size="64" />
    </v-overlay>
    <div  v-if="!loading">
      <v-row>
        <v-col>
          <track-detail-header :track="track" />
        </v-col>
      </v-row>
      <track-stat-cards :track="track" />
      <v-row>
        <v-col
          :xs="12"
          :lg="mapCols">
          <collapsable-card title="Map" textClass="pa-0">
            <main-map
              ref="mapRef"
              :center="center"
              :bounds="bounds"
              :runs="runs"
              :pistes="pistes"
              :gondola="gondola"
              :selectedRun="selectedRun"
              :zoom="zoom" />
          </collapsable-card>
        </v-col>
        <v-col id="runsContainer">
          <collapsable-card :title="runsTitle">
            <template slot="headerButtons">
              <v-btn icon @click="toggleExpandRuns">
                <v-icon>{{expandIcon}}</v-icon>
              </v-btn>
            </template>
            <run-list
              :isExpanded="runsExpanded"
              :runs="runs"
              :selectedRun="selectedRun"
              @runSelected="onRunSelected" />
          </collapsable-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <collapsable-card title="Charts" textClass="pa-0">
            <height-profile :runs="runs" :selectedRun="selectedRun" />
          </collapsable-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col :cols="6">
          <collapsable-card title="Piste difficulties">
            <difficulty-pie-chart
              :showLegend="true"
              :trackId="parseInt(trackId)" />
          </collapsable-card>
        </v-col>
        <v-col :cols="6">
          <collapsable-card title="Gondola types">
            <template slot="headerButtons">
              <button-bar v-model="selectedProperty" :items="propertyItems" />
            </template>
            <gondola-types-pie
              :showLegend="true"
              :propertyName="selectedPropertyName"
              :trackId="parseInt(trackId)" />
          </collapsable-card>
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<script>
import RunList from '../components/analysis/RunList.vue';
import DifficultyPieChart from '../components/charts/DifficultyPieChart.vue';
import GondolaTypesPie from '../components/charts/GondolaTypesPie.vue';
import HeightProfile from '../components/charts/HeightProfile.vue';
import ButtonBar from '../components/common/ButtonBar.vue';
import CollapsableCard from '../components/common/CollapsableCard.vue';
import MainMap from '../components/map/MainMap.vue';
import TrackDetailHeader from '../components/tracks/TrackDetailHeader.vue';
import TrackStatCards from '../components/tracks/TrackStatCards.vue';
import DataService from '../services/DataService';

export default {
  name: 'MapOverview',
  props: {
    trackId: String,
  },
  components: {
    MainMap,
    TrackDetailHeader,
    TrackStatCards,
    RunList,
    HeightProfile,
    CollapsableCard,
    DifficultyPieChart,
    GondolaTypesPie,
    ButtonBar,
  },
  data: () => ({
    zoom: 10,
    runsExpanded: false,
    center: [46.98, 10.3],
    gondolas: [],
    pistes: [],
    analysisResult: null,
    track: null,
    selectedRun: null,
    gondola: null,
    loading: true,
    selectedProperty: 1,
    propertyItems: [
      { id: 1, name: 'Type', key: 'type' },
      { id: 2, name: 'Heating', key: 'heating' },
      { id: 3, name: 'Occupancy', key: 'occupancy' },
    ],
  }),
  methods: {
    async fetchTrack() {
      this.loading = true;
      this.track = await DataService.getTrack(this.trackId);
      this.loading = false;
    },
    async fetchAnalysisResult() {
      this.analysisResult = await DataService.getAnalysisResult(this.trackId);
      console.log(this.analysisResult);
    },
    async fetchGondolaDetails(id) {
      this.gondola = await DataService.getGondola(id);
    },
    onRunSelected(run) {
      this.gondola = null;
      this.selectedRun = run;
      if (!this.selectedRun.downhill) {
        this.fetchGondolaDetails(this.selectedRun.gondola.id);
      }
    },
    toggleExpandRuns() {
      this.runsExpanded = !this.runsExpanded;
      this.$refs.mapRef.resize();
      if (this.runsExpanded) {
        setTimeout(() => {
          this.$vuetify.goTo('#runsContainer', {
            duration: 500,
            easing: 'easeInOutCubic',
          });
        }, 500);
      }
    },
  },
  watch: {
    async bounds() {
      const response = await DataService.getPistes(this.bounds);
      this.pistes = response.pistes;
    },
  },
  computed: {
    runs() {
      if (this.analysisResult === null) return [];
      return this.analysisResult.runs;
    },
    bounds() {
      if (this.analysisResult === null) return null;
      return this.analysisResult.bounds;
    },
    downhillRuns() {
      return this.runs.filter((x) => x.downhill);
    },
    runsTitle() {
      return `Runs (${this.downhillRuns.length})`;
    },
    expandIcon() {
      return this.runsExpanded ? 'mdi-arrow-collapse' : 'mdi-arrow-expand';
    },
    mapCols() {
      return this.runsExpanded ? 12 : 8;
    },
    selectedPropertyName() {
      return this.propertyItems.filter((x) => x.id === this.selectedProperty)[0].key;
    },
  },
  mounted() {
    this.fetchTrack();
    this.fetchAnalysisResult();
  },
};
</script>
