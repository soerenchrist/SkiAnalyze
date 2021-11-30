<template>
  <pie-chart
    :tooltip="tooltip"
    :seriesData="series"
    :palettes="palettes"
    :showLegend="showLegend"
    :height="height" />
</template>

<script>
import DataService from '../../../services/DataService';
import PieChart from '../../charts/PieChart.vue';

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
    tooltip: { enable: true },
    palettes: [],
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
      const stats = await DataService.getGondolaTypesStats(trackId);
      await this.buildSeries(stats);
    },
    buildSeries(stats) {
      const s = [];
      stats.forEach((stat) => {
        s.push({
          key: this.getName(stat.key),
          value: stat.value,
        });
      });
      this.series = s;
    },
    getName(key) {
      switch (key) {
        case 'chair_lift': return 'Chair lift';
        case 'gondola': return 'Gondola';
        case 'cable_car': return 'Cable car';
        default: return key;
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
