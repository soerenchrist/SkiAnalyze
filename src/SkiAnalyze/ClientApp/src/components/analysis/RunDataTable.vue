<template>
<div style="overflow-y: scroll; height:550px;">
  <v-data-table
    :headers="headers"
    :items="runs"
    @click:row="itemSelected"
    :item-class="itemClasses"
    :items-per-page="100">
    <template v-slot:item.downhill="{ item }">
      <v-icon>{{item.downhill ? 'mdi-ski' : 'mdi-gondola'}}</v-icon>
    </template>
    <template v-slot:item.number="{ item }">
      {{getName(item)}}
    </template>
    <template v-slot:item.totalDistance="{ item }">
      {{formatDistance(item.totalDistance)}}
    </template>
    <template v-slot:item.totalElevation="{ item }">
      {{formatElevation(item.totalElevation)}}
    </template>
    <template v-slot:item.averageSpeed="{ item }">
      {{formatSpeed(item.averageSpeed)}}
    </template>
    <template v-slot:item.maxSpeed="{ item }">
      {{formatSpeed(item.maxSpeed)}}
    </template>
    <template v-slot:item.averageHeartRate="{ item }">
      <span v-if="item.averageHeartRate">
      {{item.averageHeartRate}} bpm
      </span>
    </template>
    <template v-slot:item.maxHeartRate="{ item }">
      <span v-if="item.maxHeartRate">
      {{item.maxHeartRate}} bpm
      </span>
    </template>
  </v-data-table>
</div>
</template>

<script>

export default {
  props: {
    runs: Array,
    selectedRun: Object,
  },
  data: () => ({
    headers: [
      {
        text: '',
        align: 'start',
        sortable: false,
        value: 'downhill',
      },
      { text: 'Description', value: 'number' },
      { text: 'Distance', value: 'totalDistance' },
      { text: 'Elevation', value: 'totalElevation' },
      { text: 'Avg. Speed', value: 'averageSpeed' },
      { text: 'Max. Speed', value: 'maxSpeed' },
      { text: 'Avg. Heart rate', value: 'averageHeartRate' },
      { text: 'Max Heart rate', value: 'maxHeartRate' },
    ],
  }),
  methods: {
    getName(item) {
      if (item.downhill) return `Descent ${item.number}`;
      return item.gondola.name;
    },
    formatDistance(distance) {
      return `${(distance / 1000).toFixed(2)} km`;
    },
    itemSelected(item) {
      this.$emit('runSelected', item);
    },
    formatElevation(elevation) {
      return `${elevation.toFixed(2)} m`;
    },
    getStyle(run) {
      return `background-color: ${run.color}`;
    },
    formatSpeed(speed) {
      return `${(speed * 3.6).toFixed(2)} km/h`;
    },
    itemClasses(item) {
      if (item === this.selectedRun) return 'grey lighten-1';
      return '';
    },
  },
};
</script>
