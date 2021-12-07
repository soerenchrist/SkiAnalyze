<template>
<div>
  <v-overlay :value="analysisPending">
    <v-progress-circular indeterminate size="64" />
  </v-overlay>
  <v-row>
    <v-col>
      <v-card>
        <v-card-title>Your tracks</v-card-title>
        <v-card-text>
          <tracks-data-table
            :loading="loading"
            :tracks="tracks"
            @trackSelected="onTrackSelected" />
        </v-card-text>
        <v-card-actions>
          <v-btn text @click="addTrack">Add track</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
  <add-track-dialog v-model="showCreateDialog" @addTrack="onAddTrack" />
</div>
</template>

<script>
import AddTrackDialog from '../components/dialogs/AddTrackDialog.vue';
import TracksDataTable from '../components/tracks/TracksDataTable.vue';
import DataService from '../services/DataService';

export default {
  components: {
    TracksDataTable,
    AddTrackDialog,
  },
  data: () => ({
    tracks: [],
    loading: true,
    analysisPending: false,
    showCreateDialog: false,
  }),
  methods: {
    async fetchTracks() {
      this.loading = true;
      this.tracks = await DataService.getTracks();
      this.loading = false;
    },
    onTrackSelected(track) {
      this.$router.push(`/tracks/${track.id}`);
    },
    addTrack() {
      this.showCreateDialog = true;
    },
    async onAddTrack(track) {
      this.analysisPending = true;
      const createdTrack = await DataService.createTrack(track);
      await DataService.startAnalysis(createdTrack.id);

      const interval = setInterval(async () => {
        const status = await DataService.getAnalysisStatus(createdTrack.id);
        if (status.isFinished) {
          this.analysisPending = false;
          this.fetchTracks();
          clearInterval(interval);
        }
      }, 1000);
    },
  },
  mounted() {
    this.fetchTracks();
  },
};
</script>
