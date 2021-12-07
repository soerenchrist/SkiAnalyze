<template>
<ejs-chart
  v-if="runs && this.runs.length > 0"
  id="heightProfile"
  :primaryXAxis="xAxis"
  :axes="axes"
  :rows="rows"
  :primaryYAxis="primaryYAxis"
  :crosshair="crosshair"
  :palettes="palettes"
  :tooltip="tooltip">
  <e-series-collection>
    <e-series
      :dataSource="heightProfile"
      type="Area"
      :marker="marker"
      xName="time"
      yName="elevation"
      yAxisName="elevationAxis"
      name="Height profile" />
    <e-series
      :dataSource="speedProfile"
      type="Area"
      xName="time"
      yName="speed"
      yAxisName="speedAxis"
      name="Speed" />
    <e-series
      :dataSource="heartProfile"
      type="Area"
      xName="time"
      yName="heartRate"
      yAxisName="heartRateAxis"
      name="HeartRate" />
  </e-series-collection>
</ejs-chart>
</template>

<script>
import {
  AreaSeries,
  Crosshair,
  DateTime,
  LineSeries,
  Tooltip,
} from '@syncfusion/ej2-vue-charts';

export default {
  props: {
    runs: Array,
    selectedRun: Object,
  },
  data: () => ({
    xAxis: {
      valueType: 'DateTime',
    },
    primaryYAxis: {
      title: 'Elevation in m',
    },
    axes: [
      {
        rowIndex: 0,
        name: 'elevationAxis',
        title: 'Elevation in m',
        labelFormat: '{value} m',
      },
      {
        rowIndex: 1,
        opposedPosition: true,
        name: 'speedAxis',
        title: 'Speed in km/h',
        labelFormat: '{value} km/h',
      },
      {
        name: 'heartRateAxis',
        minimum: 50,
        maximum: 200,
        rowIndex: 2,
        title: 'Heart rate in bpm',
        labelFormat: '{value} bpm',
      },
    ],
    rows: [
      {
        height: '33%',
      },
      {
        height: '33%',
      },
      {
        height: '33%',
      },
    ],
    sampleFactor: 2,
    crosshair: { enable: true, lineType: 'Vertical' },
    tooltip: { enable: true, shared: true },
    marker: { visible: false },
    palettes: ['#3F51B5', '#4CAF50', '#E53935'],
  }),
  computed: {
    heightProfile() {
      if (this.selectedRun && this.selectedRun.downhill) {
        const series = [];
        this.getRunPoints(this.selectedRun, 0, series);
        return series;
      }

      if (this.runs) {
        const series = [];
        this.runs.forEach((run) => {
          this.getRunPoints(run, this.sampleFactor, series);
        });
        return series;
      }
      return [];
    },
    speedProfile() {
      if (this.selectedRun && this.selectedRun.downhill) {
        const series = [];
        this.getRunSpeeds(this.selectedRun, 0, series);
        return series;
      }

      if (this.runs) {
        const series = [];
        this.runs.forEach((run) => {
          this.getRunSpeeds(run, this.sampleFactor, series);
        });
        return series;
      }
      return [];
    },
    heartProfile() {
      if (this.selectedRun && this.selectedRun.downhill) {
        const series = [];
        this.getRunHeartRates(this.selectedRun, 0, series);
        return series;
      }
      if (this.runs) {
        const series = [];
        this.runs.forEach((run) => {
          this.getRunHeartRates(run, this.sampleFactor, series);
        });
        return series;
      }
      return [];
    },
    minMaxHeight() {
      if (this.heightProfile) {
        let minimum = 100000;
        let maximum = 0;
        this.heightProfile.forEach((x) => {
          if (x.elevation < minimum) {
            minimum = x.elevation;
          }
          if (x.elevation > maximum) {
            maximum = x.elevation;
          }
        });
        minimum -= 100;
        maximum += 100;
        minimum = Math.floor(minimum / 50) * 50;
        maximum = Math.floor(maximum / 50) * 50;
        return {
          minimum: parseInt(minimum, 10),
          maximum: parseInt(maximum, 10),
        };
      }
      return { minimum: 0, maximum: 3500 };
    },
  },
  watch: {
    minMaxHeight() {
      this.primaryYAxis.minimum = this.minMaxHeight.minimum;
      this.primaryYAxis.maximum = this.minMaxHeight.maximum;
    },
  },
  methods: {
    getRunPoints(run, sampleFactor, arr) {
      run.coordinates.forEach((x, index) => {
        if (index % sampleFactor === 0) return;
        arr.push({
          time: x.dateTime,
          elevation: x.elevation,
        });
      });
    },
    getRunSpeeds(run, sampleFactor, arr) {
      run.coordinates.forEach((x, index) => {
        if (index % sampleFactor === 0) return;
        arr.push({
          time: x.dateTime,
          speed: (x.speed * 3.6).toFixed(2),
        });
      });
    },
    getRunHeartRates(run, sampleFactor, arr) {
      run.coordinates.forEach((x, index) => {
        if (index % this.sampleFactor === 0) return;
        arr.push({
          time: x.dateTime,
          heartRate: x.heartRate,
        });
      });
    },
  },
  provide: {
    chart: [AreaSeries, LineSeries, DateTime, Crosshair, Tooltip],
  },
};
</script>

<style scoped>
#heightProfile {
  height: 450px;
}
</style>
