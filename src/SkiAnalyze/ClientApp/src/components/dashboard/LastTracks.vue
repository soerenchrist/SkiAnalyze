<template>
  <v-card>
    <v-card-title>Last tracks</v-card-title>
    <v-card-text>
      <tracks-data-table
        :loading="loading"
        :tracks="tracks"
        :hideFooter="true" />
    </v-card-text>
  </v-card>
</template>

<script>
import DataService from '../../services/DataService';
import TracksDataTable from '../tracks/TracksDataTable.vue';

export default {
  components: { TracksDataTable },
  data: () => ({
    tracks: [],
    loading: true,
  }),
  methods: {
    async fetchTracks() {
      this.loading = true;
      this.tracks = await DataService.getTracks();
      this.loading = false;
    },
  },
  mounted() {
    this.fetchTracks();
  },
};
</script>
