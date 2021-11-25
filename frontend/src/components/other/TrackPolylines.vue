<template>
<div v-if="hasResult">
  <l-polyline
    :lat-lngs="latLngs(line)"
    color="yellow"
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
  },
  methods: {
    latLngs(line) {
      if (!this.hasResult) return [];
      console.log(line.coordinates);
      return line.coordinates.map((x) => [x.latitude, x.longitude]);
    },
  },
};
</script>
