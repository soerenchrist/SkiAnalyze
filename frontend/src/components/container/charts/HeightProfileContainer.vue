<template>
  <div v-if="hasAnalysisResult">
    <height-profile :seriesData="seriesData" />
  </div>
</template>

<script>
import HeightProfile from '../../charts/HeightProfile.vue';

export default {
  components: {
    HeightProfile,
  },
  computed: {
    selectedRun() {
      return this.$store.getters.selectedRun;
    },
    hasSelected() {
      return this.selectedRun !== null && this.selectedGondola !== null;
    },
    analysisResult() {
      return this.$store.getters.analysisResult;
    },
    hasAnalysisResult() {
      console.log(this.analysisResult);
      return this.analysisResult !== null;
    },
    seriesData() {
      const series = [];

      if (this.hasSelected) {
        this.getRunPoints(this.selectedRun, series);
      } else {
        this.analysisResult.runs.forEach((run) => {
          this.getRunPoints(run, series);
        });
      }
      return series;
    },
  },
  methods: {
    getRunPoints(run, arr) {
      run.coordinates.forEach((x) => {
        arr.push({
          time: x.dateTime,
          elevation: x.elevation,
        });
      });
    },
  },
};
</script>
