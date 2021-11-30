<template>
  <pie-chart
    v-if="!loading"
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
    propertyName: {
      type: String,
      required: false,
      default: 'type',
    },
  },
  data: () => ({
    series: [],
    tooltip: { enable: true },
    palettes: [],
    loading: false,
  }),
  watch: {
    trackId() {
      if (this.trackId !== null) {
        this.fetchStats(this.trackId);
      }
    },
    propertyName() {
      if (this.trackId !== null) {
        this.fetchStats(this.trackId);
      }
    },
  },
  methods: {
    async fetchStats(trackId) {
      this.loading = true;
      const stats = await DataService.getGondolaCountByPropertyStats(trackId, this.propertyName);
      this.buildSeries(stats);
      this.loading = false;
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
