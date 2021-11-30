<template>
  <difficulty-pie :seriesData="series" :height="height" />
</template>

<script>
import DataService from '../../../services/DataService';
import DifficultyPie from '../../charts/DifficultyPie.vue';

export default {
  components: { DifficultyPie },
  props: {
    height: String,
    trackId: Number,
  },
  data: () => ({
    series: [],
  }),
  watch: {
    trackId() {
      if (this.trackId !== null) {
        this.fetchStats(this.trackId);
      }
    },
  },
  methods: {
    async fetchStats(trackId) {
      const stats = await DataService.getDifficultyStats(trackId);
      await this.buildSeries(stats);
    },
    buildSeries(stats) {
      const s = [];
      stats.forEach((stat) => {
        s.push({
          key: this.getName(stat.key),
          value: (stat.value * 100).toFixed(2),
        });
      });
      this.series = s;
    },
    getName(key) {
      switch (key) {
        case 0: return 'Easy';
        case 1: return 'Novice';
        case 2: return 'Intermediate';
        default: return 'Advanced';
      }
    },
  },
  mounted() {
    if (this.trackId) {
      this.fetchStats(this.trackId);
    }
  },
};
</script>
