<template>
<div v-if="hasResult">
  <l-polyline
    :lat-lngs="latLngs(line)"
    :color="getColor(line)"
    :weight="getWeight(line)"
    v-for="line in visibleRuns"
    :key="line.id" />
</div>
</template>

<script>
export default {
  computed: {
    result() {
      return this.$store.getters.analysisResult;
    },
    hasResult() {
      return this.result !== null;
    },
    runs() {
      return this.result.runs.filter((x) => x.downhill);
    },
    visibleTracks() {
      return this.$store.getters.visibleTracks;
    },
    visibleRuns() {
      let runs = [];
      this.visibleTracks.forEach((track) => {
        const visibleTracks = this.runs.filter((run) => run.trackId === track.id);
        runs = runs.concat(visibleTracks);
      });
      return runs;
    },
    selectedRun() {
      return this.$store.getters.selectedRun;
    },
  },
  methods: {
    latLngs(line) {
      if (!this.hasResult) return [];
      return line.coordinates.map((x) => [x.latitude, x.longitude]);
    },
    getColor(line) {
      if (this.selectedRun === null) return line.color;
      if (line.number === this.selectedRun.number) {
        return '#ffff00';
      }
      return line.color;
    },
    getWeight(line) {
      if (this.selectedRun === null) return 4;
      if (line.number === this.selectedRun.number) {
        return 6;
      }
      return 4;
    },
  },
};
</script>
