<template>
  <tracks
    :tracks="tracks"
    :loading="loading"
    @addTrack="onAddTrack"
    @removeTrack="onRemoveTrack" />
</template>

<script>
import { FETCH_TRACKS } from '../../store/actions';
import { SET_DISPLAY_ADD_TRACK_DIALOG } from '../../store/mutations';
import Tracks from '../Tracks.vue';

export default {
  components: { Tracks },
  computed: {
    tracks() {
      return this.$store.getters.tracks;
    },
    loading() {
      return this.$store.state.tracks.loading;
    },
  },
  methods: {
    onAddTrack() {
      this.$store.commit(SET_DISPLAY_ADD_TRACK_DIALOG, true);
    },
    onRemoveTrack() { // track) {
      // this.$store.commit(REMOVE_TRACK, track);
    },
  },
  mounted() {
    this.$store.dispatch(FETCH_TRACKS);
  },
};
</script>
