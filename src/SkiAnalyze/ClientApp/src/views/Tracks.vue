<template>
<div class="pa-4">
  <v-row>
    <v-col>
      <tracks-container />
    </v-col>
  </v-row>
  <add-track-dialog-container />
</div>
</template>

<script>
import AddTrackDialogContainer from '../components/container/dialogs/AddTrackDialogContainer.vue';
import TracksContainer from '../components/container/TracksContainer.vue';

export default {
  components: {
    TracksContainer,
    AddTrackDialogContainer,
  },
  computed: {
    tracks() {
      return this.$store.getters.tracks;
    },
    hasTracks() {
      return this.tracks.length > 0;
    },
  },
};
</script>

<style scoped>
.center {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>
