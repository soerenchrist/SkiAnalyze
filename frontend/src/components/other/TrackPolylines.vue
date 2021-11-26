<template>
<div v-if="hasResult">
  <l-polyline
    :lat-lngs="latLngs(line)"
    :color="color(line)"
    v-for="line in result.runs"
    :key="line.number" />
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
    selectedRun() {
      return this.$store.getters.selectedRun;
    },
  },
  methods: {
    latLngs(line) {
      if (!this.hasResult) return [];
      return line.coordinates.map((x) => [x.latitude, x.longitude]);
    },
    color(line) {
      if (this.selectedRun === null) return 'yellow';
      if (line.number === this.selectedRun.number) {
        return 'orange';
      }
      return 'yellow';
    },
  },
};
</script>
