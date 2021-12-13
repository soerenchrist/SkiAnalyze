<template>
<div>
  <v-overlay :value="analysisPending">
    <v-progress-circular indeterminate size="64" />
  </v-overlay>
  <v-row>
    <v-col>
      <v-card>
        <v-card-title>
          {{$t('tracks.yourTracks')}}
          <v-spacer />

          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                icon
                v-bind="attrs"
                v-on="on"
                @click="mapMode = !mapMode">
                <v-icon>{{icon}}</v-icon>
              </v-btn>
            </template>
            <span>{{$t('tracks.showMap')}}</span>
          </v-tooltip>
        </v-card-title>
        <v-card-text>
          <tracks-data-table
            :loading="loading"
            :tracks="filteredTracks"
            @trackSelected="onTrackSelected" />
        </v-card-text>
        <v-card-actions>
          <v-btn text @click="addTrack">{{$t('tracks.addTrack')}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
    <v-col v-if="mapMode">
      <v-card>
        <v-card-title>{{$t('tracks.map')}}</v-card-title>
        <v-card-text class="pa-0">
          <track-overview-map
            v-model="syncMapAndList"
            @boundsUpdated="onBoundsUpdated"
            :tracks="tracks" />
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
  <add-track-dialog v-model="showCreateDialog" @addTracks="onAddTracks" />
</div>
</template>

<script>
import AddTrackDialog from '../components/dialogs/AddTrackDialog.vue';
import TrackOverviewMap from '../components/map/TrackOverviewMap.vue';
import TracksDataTable from '../components/tracks/TracksDataTable.vue';
import DataService from '../services/DataService';

export default {
  components: {
    TracksDataTable,
    AddTrackDialog,
    TrackOverviewMap,
  },
  data: () => ({
    tracks: [],
    loading: true,
    analysisPending: false,
    showCreateDialog: false,
    mapMode: false,
    bounds: null,
    syncMapAndList: true,
  }),
  computed: {
    icon() {
      return this.mapMode ? 'mdi-map-marker-remove' : 'mdi-map-marker-radius';
    },
    filteredTracks() {
      if (!this.syncMapAndList || this.bounds === null) {
        return this.tracks;
      }

      return this.tracks.filter((t) => t.skiArea !== null
        && t.skiArea.centerLatitude > this.bounds._southWest.lat
        && t.skiArea.centerLongitude > this.bounds._southWest.lng
        && t.skiArea.centerLatitude < this.bounds._northEast.lat
        && t.skiArea.centerLongitude < this.bounds._northEast.lng);
    },
  },
  methods: {
    async fetchTracks() {
      this.loading = true;
      this.tracks = await DataService.getTracks();
      this.loading = false;
    },
    onTrackSelected(track) {
      this.$router.push({ name: 'Track', params: { trackId: track.id } });
    },
    addTrack() {
      this.showCreateDialog = true;
    },
    onBoundsUpdated(bounds) {
      this.bounds = bounds;
    },
    promiseTest() {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve(Math.random());
        }, 2000);
      });
    },
    waitForAnalysisFinish(trackId) {
      return new Promise((resolve) => {
        DataService.startAnalysis(trackId)
          .then(() => {
            const interval = setInterval(async () => {
              const status = await DataService.getAnalysisStatus(trackId);
              if (status.isFinished) {
                clearInterval(interval);
                resolve();
              }
            }, 1000);
          });
      });
    },
    async waitForTrackCreation(tracks) {
      const promises = [];

      for (let i = 0; i < tracks.length; i++) {
        const track = tracks[i];
        promises.push(DataService.createTrack(track));
      }

      const createdTracks = await Promise.all(promises);
      return createdTracks;
    },
    async onAddTracks(tracks) {
      this.analysisPending = true;

      const allPromises = [];

      // create all tracks in parallel
      const createdTracks = await this.waitForTrackCreation(tracks);

      // start analysis and wait for finish
      for (let i = 0; i < createdTracks.length; i++) {
        allPromises.push(this.waitForAnalysisFinish(createdTracks[i].id));
      }

      await Promise.all(allPromises);
      this.analysisPending = false;
      this.fetchTracks();
    },
  },
  mounted() {
    this.fetchTracks();
  },
};
</script>
