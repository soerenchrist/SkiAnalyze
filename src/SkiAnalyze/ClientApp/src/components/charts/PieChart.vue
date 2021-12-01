<template>
  <ejs-accumulationchart
    :style="getStyle()"
    :tooltip="tooltip"
    ref="chart"
    :legendSettings="legend">
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
import { PieSeries, AccumulationTooltip, AccumulationLegend } from '@syncfusion/ej2-vue-charts';

export default {
  props: {
    seriesData: Array,
    height: String,
    tooltip: Object,
    palettes: Array,
    showLegend: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  provide: {
    accumulationchart: [PieSeries, AccumulationTooltip, AccumulationLegend],
  },
  computed: {
    legend() {
      return {
        visible: this.showLegend,
      };
    },
  },
  methods: {
    getStyle() {
      if (this.height) {
        return `height: ${this.height}`;
      }
      return '';
    },
  },
};

</script>
