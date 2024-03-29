<template>
  <div>
    <loading-overlay :loading="loading" />
    <div  v-if="!loading">
      <v-row>
        <v-col>
          <track-detail-header @delete="onDelete" @showSkiArea="onShowSkiArea" :track="track" />
        </v-col>
      </v-row>
      <track-stat-cards :track="track" />
      <v-row>
        <v-col
          :xs="12"
          :lg="mapCols">
          <collapsable-card :title="$t('tracks.map')" textClass="pa-0">
            <main-map
              ref="mapRef"
              :center="center"
              :bounds="bounds"
              :runs="runs"
              :pistes="pistes"
              :gondola="gondola"
              :hoverMarker="hoverMarkerPoint"
              @runSelected="onRunSelected"
              :selectedRun="selectedRun"
              :zoom="zoom" />
          </collapsable-card>
        </v-col>
        <v-col id="runsContainer">
          <collapsable-card :title="runsTitle">
            <template slot="headerButtons">
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    icon
                    @click="toggleExpandRuns"
                    v-on="on"
                    v-bind="attrs">
                    <v-icon>{{expandIcon}}</v-icon>
                  </v-btn>
                </template>
                <span>{{tooltipText}}</span>
              </v-tooltip>
            </template>
            <run-list
              :isExpanded="runsExpanded"
              :runs="runs"
              :selectedRun="selectedRun"
              @onRemove="removeRun"
              @onAddLiftAbove="addGondolaAbove"
              @onAddLiftBelow="addGondolaBelow"
              @runSelected="onRunSelected" />
          </collapsable-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <collapsable-card :title="chartsTitle" textClass="pa-0">
            <height-profile
              :runs="runs"
              :selectedRun="selectedRun"
              @onHover="onHover" />
          </collapsable-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col :cols="4">
          <collapsable-card :title="$t('tracks.pisteDifficulties')">
            <difficulty-pie-chart
              :showLegend="true"
              :trackId="parseInt(trackId)" />
          </collapsable-card>
        </v-col>
        <v-col :cols="4">
          <collapsable-card :title="$t('tracks.gondolaTypes')">
            <template slot="headerButtons">
              <button-bar v-model="selectedProperty" :items="propertyItems" />
            </template>
            <gondola-types-pie
              :showLegend="true"
              :propertyName="selectedPropertyName"
              :trackId="parseInt(trackId)" />
          </collapsable-card>
        </v-col>
        <v-col :cols="4">
          <collapsable-card :title="$t('tracks.heartRateAverage')">
            <heart-rate-averages
              :trackId="parseInt(trackId)" />
          </collapsable-card>
        </v-col>
      </v-row>
      <select-gondola-dialog
        :open="selectGondola"
        @gondolaSelected="gondolaSelected"
        :skiAreaId="track.skiArea.id" />
    </div>
  </div>
</template>

<script>
import RunList from '../components/analysis/RunList.vue';
import DifficultyPieChart from '../components/charts/DifficultyPieChart.vue';
import GondolaTypesPie from '../components/charts/GondolaTypesPie.vue';
import HeartRateAverages from '../components/charts/HeartRateAverages.vue';
import HeightProfile from '../components/charts/HeightProfile.vue';
import ButtonBar from '../components/common/ButtonBar.vue';
import CollapsableCard from '../components/common/CollapsableCard.vue';
import MainMap from '../components/map/MainMap.vue';
import TrackDetailHeader from '../components/tracks/TrackDetailHeader.vue';
import TrackStatCards from '../components/tracks/TrackStatCards.vue';
import DataService from '../services/DataService';
import SelectGondolaDialog from '../components/dialogs/SelectGondolaDialog.vue';

export default {
  name: 'TrackDetail',
  props: ['trackId'],
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
    HeartRateAverages,
    SelectGondolaDialog,
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
    hoverMarkerPoint: null,
    propertyItems: [],
    selectGondola: false,
    addGondolaPosition: -1,
  }),
  methods: {
    async fetchTrack() {
      const response = await DataService.getTrack(this.trackId);
      if (response.status === 404) {
        this.$router.replace({ name: '404Resource', params: { resource: 'track' } });
      } else {
        this.track = response;
      }
    },
    async fetchAnalysisResult() {
      const response = await DataService.getAnalysisResult(this.trackId);
      if (response.status === 404) {
        return;
      }
      this.analysisResult = response;
    },
    async fetchGondolaDetails(id) {
      this.gondola = await DataService.getGondola(id);
    },
    onRunSelected(run) {
      this.gondola = null;
      this.selectedRun = run;
      if (this.selectedRun && !this.selectedRun.downhill) {
        this.fetchGondolaDetails(this.selectedRun.gondola.id);
      }
    },
    onShowSkiArea(skiArea) {
      this.$router.push({ name: 'SkiArea', params: { skiAreaId: skiArea.id } });
    },
    toggleExpandRuns() {
      this.runsExpanded = !this.runsExpanded;
      this.$refs.mapRef.resize();
      setTimeout(() => {
        this.$vuetify.goTo('#runsContainer', {
          duration: 500,
          easing: 'easeInOutCubic',
        });
      }, 500);
    },
    async removeRun(run) {
      await DataService.removeRun(this.track.id, run.id);
      await this.fetchTrack();
      await this.fetchAnalysisResult();
    },
    async onDelete() {
      await DataService.removeTrack(this.trackId);
      this.$router.push({ name: 'Tracks' });
    },
    onHover(point) {
      this.hoverMarkerPoint = point;
    },
    addGondolaAbove(item) {
      this.selectGondola = true;
      this.addGondolaPosition = item.sortId;
    },
    addGondolaBelow(item) {
      this.selectGondola = true;
      this.addGondolaPosition = item.sortId + 1;
    },
    async gondolaSelected(gondola) {
      if (this.addGondolaPosition === -1) return;

      await DataService.addGondolaToRuns(this.track.id, gondola.id, this.addGondolaPosition);
      this.selectGondola = false;
      this.addGondolaPosition = -1;

      await this.fetchAnalysisResult();
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
      return `${this.$t('tracks.runs')} (${this.downhillRuns.length})`;
    },
    chartsTitle() {
      const title = this.$t('tracks.charts');
      if (this.selectedRun && this.selectedRun.downhill) {
        return `${title} - ${this.$t('tracks.run')} ${this.selectedRun.number}`;
      }
      return title;
    },
    expandIcon() {
      return this.runsExpanded ? 'mdi-arrow-collapse' : 'mdi-arrow-expand';
    },
    tooltipText() {
      return this.runsExpanded ? 'Hide details' : 'Show details';
    },
    mapCols() {
      return this.runsExpanded ? 12 : 8;
    },
    selectedPropertyName() {
      return this.propertyItems.filter((x) => x.id === this.selectedProperty)[0].key;
    },
  },
  async mounted() {
    this.propertyItems = [
      { id: 1, name: this.$t('gondola.type'), key: 'type' },
      { id: 2, name: this.$t('gondola.heating'), key: 'heating' },
      { id: 3, name: this.$t('gondola.occupancy'), key: 'occupancy' },
    ];
    const promises = [];
    this.loading = true;
    promises.push(this.fetchTrack());
    promises.push(this.fetchAnalysisResult());
    await Promise.all(promises);
    this.loading = false;
  },
};
</script>
