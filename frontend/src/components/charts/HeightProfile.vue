<template>
<ejs-chart
  v-if="seriesData && seriesData.length > 0"
  id="heightProfile"
  :primaryXAxis="xAxis"
  :primaryYAxis="yAxis"
  :crosshair="crosshair"
  :palettes="palettes"
  :tooltip="tooltip">
  <e-series-collection>
    <e-series
      :dataSource="seriesData"
      type="Area"
      :marker="marker"
      xName="time"
      yName="elevation"
      name="Height profile" />
  </e-series-collection>
</ejs-chart>
</template>

<script>
import {
  AreaSeries,
  Crosshair,
  DateTime,
  Tooltip,
} from '@syncfusion/ej2-vue-charts';

export default {
  props: {
    seriesData: Array,
  },
  data: () => ({
    xAxis: {
      valueType: 'DateTime',
    },
    yAxis: {
      title: 'elevation',
    },
    crosshair: { enable: true, lineType: 'Vertical' },
    tooltip: { enable: true, shared: true, format: 'Elevation: ${point.y} m' },
    marker: { visible: false },
    palettes: ['#42A5F5', '#26A69A', '#66BB6A', '#D4E157'],
  }),
  provide: {
    chart: [AreaSeries, DateTime, Crosshair, Tooltip],
  },
};
</script>

<style scoped>
#heightProfile {
  height: 300px;
}
</style>
