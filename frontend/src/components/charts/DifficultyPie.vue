<template>
  <ejs-accumulationchart id="difficultyPie" :tooltip="tooltip">
    <e-accumulation-series-collection>
      <e-accumulation-series
        :dataSource="seriesData"
        :palettes="palettes"
        xName="key"
        yName="value" />
    </e-accumulation-series-collection>
  </ejs-accumulationchart>
</template>

<script>
import { PieSeries, AccumulationTooltip } from '@syncfusion/ej2-vue-charts';

export default {
  props: {
    seriesData: Array,
  },
  data: () => ({
    tooltip: { enable: true, format: '${point.x}: <b>${point.y}%</b>' },
    palettes: ['#3F51B5', '#2196F3', '#F44336', '#212121'],
  }),
  provide: {
    accumulationchart: [PieSeries, AccumulationTooltip],
  },
};

</script>

<style>
#difficultyPie {
  height: 250px;
}
</style>
