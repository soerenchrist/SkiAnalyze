<template>
  <tracks
    :tracks="tracks"
    :loading="loading"
    @addTrack="onAddTrack"
    @removeTrack="onRemoveTrack"
    @toggleVisibility="toggleVisibility" />
</template>

<script>
import { FETCH_TRACKS, REMOVE_TRACK } from '../../store/actions';
import { SET_DISPLAY_ADD_TRACK_DIALOG, TOGGLE_TRACK_VISIBILTIY } from '../../store/mutations';
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
    onRemoveTrack(track) {
      this.$store.dispatch(REMOVE_TRACK, track.id);
    },
    toggleVisibility(track) {
      this.$store.commit(TOGGLE_TRACK_VISIBILTIY, track);
    },
  },
  mounted() {
    this.$store.dispatch(FETCH_TRACKS);
  },
};
</script>
