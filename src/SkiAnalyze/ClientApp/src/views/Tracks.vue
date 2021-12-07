<template>
<div>
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
  <add-track-dialog v-model="showCreateDialog" />
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
  },
  mounted() {
    this.fetchTracks();
  },
};
</script>
