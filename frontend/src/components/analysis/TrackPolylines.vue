<template>
<div v-if="hasResult">
  <l-polyline
    :lat-lngs="latLngs(line)"
    :color="line.color"
    :weight="getWeight(line)"
    v-for="line in runs"
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
    selectedRun() {
      return this.$store.getters.selectedRun;
    },
  },
  methods: {
    latLngs(line) {
      if (!this.hasResult) return [];
      return line.coordinates.map((x) => [x.latitude, x.longitude]);
    },
    getWeight(line) {
      if (this.selectedRun === null) return 2;
      if (line.number === this.selectedRun.number) {
        return 6;
      }
      return 2;
    },
  },
};
</script>
