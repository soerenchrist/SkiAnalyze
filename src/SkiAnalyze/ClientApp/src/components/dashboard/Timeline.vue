<template>
  <v-card>
    <v-card-title>
      Timeline
      <v-spacer />
      <year-selector
        v-if="selectedDateRange === 0"
        :maxYear="new Date().getFullYear()"
        :minYear="2000"
        v-model="year" />
      <v-divider class="d-none d-md-block mx-3" vertical v-if="selectedDateRange === 0" />
      <button-bar class="d-none d-md-block" :items="dateRanges" v-model="selectedDateRange" />
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
import YearSelector from '../common/YearSelector.vue';

export default {
  components: { ButtonBar, YearSelector },
  data: () => ({
    selectedProperty: 0,
    selectedDateRange: 0,
    year: new Date().getFullYear(),
    timeline: [],
    tooltip: { enable: true },
    crosshair: { enable: true, lineType: 'Vertical' },
    marker: { visible: false },
    propertyNames: [
      { id: 0, name: 'Distance', key: 'distance' },
      { id: 1, name: 'Descent', key: 'elevation' },
      { id: 2, name: 'Calories', key: 'calories' },
    ],
    dateRanges: [
      { id: 0, name: 'One year', key: 'distance' },
      { id: 1, name: '6 months', key: '6months' },
      { id: 2, name: '3 months', key: '3months' },
      { id: 3, name: '1 month', key: 'month' },
      { id: 4, name: '1 week', key: 'week' },
    ],
  }),
  methods: {
    async fetchTimeline() {
      this.timeline = await DataService.getTimeline(this.propertyName, this.dateRange, this.year);
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
    dateRange() {
      return this.dateRanges[this.selectedDateRange].key;
    },
    xAxis() {
      return {
        valueType: 'DateTime',
        intervalType: this.intervalType,
      };
    },
    intervalType() {
      switch (this.selectedDateRange) {
        case 0:
          return 'Months';
        default:
          return 'Days';
      }
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
        title: 'Distance over time',
        labelFormat: this.format,
      };
    },
  },
  watch: {
    selectedProperty() {
      this.fetchTimeline();
    },
    selectedDateRange() {
      this.fetchTimeline();
    },
    year() {
      this.fetchTimeline();
    },
  },
  mounted() {
    this.fetchTimeline();
  },
  provide: {
    chart: [AreaSeries, DateTime, Tooltip, Crosshair],
  },
};
</script>
