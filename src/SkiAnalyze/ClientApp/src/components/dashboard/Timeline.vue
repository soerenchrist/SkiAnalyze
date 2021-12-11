<template>
  <v-card>
    <v-card-title>
      Timeline
      <v-spacer />
      <date-range-selector @dateChanged="onDatesChanged" />
      <v-divider class="d-none d-lg-block mx-3" vertical />
      <button-bar class="d-none d-lg-block" :items="propertyNames" v-model="selectedProperty" />
    </v-card-title>
    <v-card-text class="pa-0">
      <ejs-chart v-if="timeline"
        :primaryXAxis="xAxis"
        :crosshair="crosshair"
        :primaryYAxis="yAxis"
        :tooltip="tooltip">
        <e-series-collection>
          <e-series
            :dataSource="seriesData"
            type="Area"
            xName="time"
            :marker="marker"
            yName="value" />
        </e-series-collection>
      </ejs-chart>
    </v-card-text>
  </v-card>
</template>

<script>
import {
  AreaSeries,
  DateTime,
  Crosshair,
  Tooltip,
} from '@syncfusion/ej2-vue-charts';
import DataService from '../../services/DataService';
import ButtonBar from '../common/ButtonBar.vue';
import DateRangeSelector from '../common/DateRangeSelector.vue';

export default {
  components: { ButtonBar, DateRangeSelector },
  data: () => ({
    selectedProperty: 0,
    dateRange: null,
    timeline: [],
    tooltip: { enable: true },
    crosshair: { enable: true, lineType: 'Vertical' },
    marker: { visible: false },
    propertyNames: [],
  }),
  methods: {
    async fetchTimeline() {
      this.timeline = await DataService.getTimeline(this.propertyName, this.dateRange);
    },
    onDatesChanged(range) {
      this.dateRange = range;
    },
  },
  computed: {
    seriesData() {
      let divider = 1;
      if (this.selectedProperty === 0) {
        divider = 1000;
      } else if (this.selectedProperty === 1) {
        divider = -1000;
      }
      return this.timeline.map((x) => ({
        time: new Date(x.key),
        value: x.value / divider,
      }));
    },
    propertyName() {
      return this.propertyNames[this.selectedProperty].key;
    },
    xAxis() {
      return {
        valueType: 'DateTime',
        intervalType: 'Months',
      };
    },
    yAxisTitle() {
      switch (this.selectedProperty) {
        case 1:
          return 'Descent in km';
        case 2:
          return 'Calories in kcal';
        default:
          return 'Distance in km';
      }
    },
    format() {
      switch (this.selectedProperty) {
        case 1:
          return '{value} km';
        case 2:
          return '{value} kcal';
        default:
          return '{value} km';
      }
    },
    yAxis() {
      return {
        title: this.yAxisTitle,
        labelFormat: this.format,
      };
    },
  },
  watch: {
    selectedProperty() {
      this.fetchTimeline();
    },
    year() {
      this.fetchTimeline();
    },
    dateRange() {
      this.fetchTimeline();
    },
  },
  mounted() {
    this.propertyNames = [
      { id: 0, name: this.$t('dashboard.distance'), key: 'distance' },
      { id: 1, name: this.$t('dashboard.descent'), key: 'elevation' },
      { id: 2, name: this.$t('dashboard.calories'), key: 'calories' },
    ];
    this.fetchTimeline();
  },
  provide: {
    chart: [AreaSeries, DateTime, Tooltip, Crosshair],
  },
};
</script>
