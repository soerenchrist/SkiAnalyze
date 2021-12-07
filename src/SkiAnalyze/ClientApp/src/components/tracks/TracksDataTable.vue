<template>
  <v-data-table
    :headers="computedHeaders"
    :items="tracks"
    @click:row="itemSelected"
    :items-per-page="100"
    :loading="loading"
    :hide-default-footer="hideFooter"
    :hide-default-header="hideHeader"
    :item-class="itemClasses"
  >
    <template v-slot:item.date="{ item }">
      {{ formatDate(item.date) }}
    </template>
    <template v-slot:item.totalDistance="{ item }">
      {{ formatDistance(item.totalDistance) }}
    </template>
    <template v-slot:item.totalElevation="{ item }">
      {{ formatElevation(item.totalElevation) }}
    </template>
    <template v-slot:item.averageSpeed="{ item }">
      {{ formatSpeed(item.averageSpeed) }}
    </template>
    <template v-slot:item.maxSpeed="{ item }">
      <span class="d-none d-xl-flex">
      {{ formatSpeed(item.maxSpeed) }}
      </span>
    </template>
    <template v-slot:item.averageHeartRate="{ item }">
      <span class="d-none d-xl-flex">
        {{ formatHeartRate(item.averageHeartRate) }}
      </span>
    </template>
  </v-data-table>
</template>

<script>
export default {
  props: {
    loading: Boolean,
    tracks: Array,
    selectedTrack: Object,
    hideHeader: {
      required: false,
      default: false,
      type: Boolean,
    },
    hideFooter: {
      required: false,
      default: false,
      type: Boolean,
    },
  },
  data: () => ({
    headers: [
      { text: 'Name', value: 'name' },
      { text: 'Ski area', value: 'skiArea.name' },
      { text: 'Date', value: 'date' },
      { text: 'Distance', value: 'totalDistance' },
      { text: 'Elevation', value: 'totalElevation' },
      { text: 'Avg. Speed', value: 'averageSpeed' },
      { text: 'Max Speed', value: 'maxSpeed', class: ['d-none', 'd-xl-table-cell'] },
      { text: 'Avg. HeartRate', value: 'averageHeartRate', class: ['d-none', 'd-xl-table-cell'] },
    ],
  }),
  methods: {
    isSelected(track) {
      return this.selectedTrack && this.selectedTrack.id === track.id;
    },
    itemSelected(track) {
      this.$emit('trackSelected', track);
    },
    formatDate(date) {
      return new Date(date).toDateString();
    },
    formatDistance(distance) {
      return `${(distance / 1000).toFixed(2)} km`;
    },
    formatElevation(elevation) {
      return `${-elevation.toFixed(2)} m`;
    },
    formatSpeed(speed) {
      return `${(speed * 3.6).toFixed(2)} km/h`;
    },
    formatHeartRate(heartRate) {
      return `${parseInt(heartRate, 10)} bpm`;
    },
    itemClasses(item) {
      if (this.isSelected(item)) return 'grey lighten-1';
      return '';
    },
  },
  computed: {
    computedHeaders() {
      if (this.hideDelete) {
        return this.headers.filter((x) => x.value !== 'delete');
      }
      return this.headers;
    },
  },
};
</script>
