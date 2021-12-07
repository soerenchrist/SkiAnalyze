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
            <v-card-text class="pa-0">
              <height-profile :runs="runs" :selectedRun="selectedRun" />
            </v-card-text>
          </collapsable-card>
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<script>
import RunList from '../components/analysis/RunList.vue';
import HeightProfile from '../components/charts/HeightProfile.vue';
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
  },
  mounted() {
    this.fetchTrack();
    this.fetchAnalysisResult();
  },
};
</script>
