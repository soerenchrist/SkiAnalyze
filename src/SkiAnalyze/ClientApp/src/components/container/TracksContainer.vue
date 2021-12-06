<template>
  <tracks
    :tracks="tracks"
    :loading="loading"
    @addTrack="onAddTrack"
    @removeTrack="onRemoveTrack"
    :selectedTrack="selectedTrack"
    @trackSelected="onTrackSelected"/>
</template>

<script>
import { FETCH_TRACKS, REMOVE_TRACK } from '../../store/actions';
import { DISPLAY_ADD_TRACK_DIALOG } from '../../store/mutations';
import Tracks from '../tracks/TracksCard.vue';

export default {
  components: { Tracks },
  computed: {
    tracks() {
      return this.$store.getters.tracks;
    },
    loading() {
      return this.$store.state.tracks.loading;
    },
    selectedTrack() {
      return this.$store.getters.selectedTrack;
    },
  },
  methods: {
    onAddTrack() {
      this.$store.commit(DISPLAY_ADD_TRACK_DIALOG, true);
    },
    onRemoveTrack(track) {
      this.$store.dispatch(REMOVE_TRACK, track.id);
    },
    onTrackSelected(track) {
      this.$router.push(`/tracks/${track.id}`);
    },
  },
  mounted() {
    if (this.track === null || this.tracks.length === 0) {
      this.$store.dispatch(FETCH_TRACKS);
    }
  },
};
</script>
