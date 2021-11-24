<template>
  <tracks
    :tracks="tracks"
    @addTrack="onAddTrack"
    @removeTrack="onRemoveTrack" />
</template>

<script>
import { REMOVE_TRACK, SET_DISPLAY_ADD_TRACK_DIALOG } from '../../store/mutations';
import Tracks from '../Tracks.vue';

export default {
  components: { Tracks },
  computed: {
    tracks() {
      return this.$store.state.tracks;
    },
  },
  methods: {
    onAddTrack() {
      this.$store.commit(SET_DISPLAY_ADD_TRACK_DIALOG, true);
    },
    onRemoveTrack(track) {
      this.$store.commit(REMOVE_TRACK, track);
    },
  },
};
</script>
