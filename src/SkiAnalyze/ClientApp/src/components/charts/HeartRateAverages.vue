<template>
  <ejs-chart
    :tooltip="tooltip"
    :primaryXAxis="xAxis"
    :primaryYAxis="yAxis"
    :pointRender="pointRender">
    <e-series-collection>
      <e-series
        :dataSource="seriesData"
        type="Column"
        xName="difficulty"
        yName="heartRate"
        name="Average Heart rate" />
    </e-series-collection>
  </ejs-chart>
</template>

<script>
import { ColumnSeries, Category, Tooltip } from '@syncfusion/ej2-vue-charts';
import DataService from '../../services/DataService';

export default {
  props: {
    trackId: Number,
  },
  data: () => ({
    tooltip: {
      enable: true,
    },
    seriesData: [],
  }),
  computed: {
    yAxis() {
      return {
        title: this.$t('tracks.avgHeartRate'),
      };
    },
    xAxis() {
      return {
        valueType: 'Category',
        title: this.$t('tracks.pisteDifficulties'),
      };
    },
  },
  methods: {
    async fetchStats() {
      const stats = await DataService.getHeartRatesPerPisteDifficulty(this.trackId);
      this.seriesData = stats.map((y) => ({
        difficulty: this.mapDifficulty(y.key),
        heartRate: y.value,
      }));
    },
    pointRender(args) {
      switch (args.point.x) {
        case this.$t('pistes.easy'):
          args.fill = '#3F51B5';
          break;
        case this.$t('pistes.novice'):
          args.fill = '#2196F3';
          break;
        case this.$t('pistes.intermediate'):
          args.fill = '#F44336';
          break;
        case this.$t('pistes.advanced'):
          args.fill = '#212121';
          break;
        default:
          break;
      }
    },
    mapDifficulty(diff) {
      switch (diff) {
        case 0:
          return this.$t('pistes.easy');
        case 1:
          return this.$t('pistes.novice');
        case 2:
          return this.$t('pistes.intermediate');
        default:
          return this.$t('pistes.advanced');
      }
    },
  },
  watch: {
    trackId() {
      this.fetchStats();
    },
  },
  mounted() {
    this.fetchStats();
  },
  provide: {
    chart: [ColumnSeries, Category, Tooltip],
  },
};
</script>
