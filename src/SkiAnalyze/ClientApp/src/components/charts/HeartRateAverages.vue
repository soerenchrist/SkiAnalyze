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
    xAxis: {
      valueType: 'Category',
      title: 'Piste difficulty',
    },
    yAxis: {
      title: 'Average Heart rate',
    },
    tooltip: {
      enable: true,
    },
    seriesData: [],
  }),
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
        case 'Easy':
          args.fill = '#3F51B5';
          break;
        case 'Novice':
          args.fill = '#2196F3';
          break;
        case 'Intermediate':
          args.fill = '#F44336';
          break;
        case 'Advanced':
          args.fill = '#212121';
          break;
        default:
          break;
      }
    },
    mapDifficulty(diff) {
      switch (diff) {
        case 0:
          return 'Easy';
        case 1:
          return 'Novice';
        case 2:
          return 'Intermediate';
        default:
          return 'Advanced';
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
