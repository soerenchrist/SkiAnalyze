<template>
  <pie-chart
    :tooltip="tooltip"
    :palettes="palettes"
    :seriesData="series"
    :showLegend="showLegend"
    :height="height" />
</template>

<script>
import DataService from '../../services/DataService';
import PieChart from './PieChart.vue';

export default {
  components: { PieChart },
  props: {
    height: String,
    trackId: Number,
    showLegend: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  data: () => ({
    series: [],
    tooltip: { enable: true, format: '${point.x}: <b>${point.y}%</b>' },
    palettes: ['#3F51B5', '#2196F3', '#F44336', '#212121'],
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
