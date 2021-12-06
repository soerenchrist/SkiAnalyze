<template>
  <v-card>
    <v-card-title>
      Tracks
    </v-card-title>
    <v-card-text>
      <tracks-data-table
        :tracks="tracks"
        :loading="loading"
        @trackSelected="onTrackSelected" />
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="onAdd">Add track</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import TracksDataTable from './TracksDataTable.vue';

export default {
  props: {
    tracks: Array,
    loading: Boolean,
    selectedTrack: Object,
  },
  components: { TracksDataTable },
  methods: {
    onAdd() {
      this.$emit('addTrack');
    },
    onRemove(track) {
      this.$emit('removeTrack', track);
    },
    onTrackSelected(track) {
      this.$emit('trackSelected', track);
    },
  },
};
</script>
