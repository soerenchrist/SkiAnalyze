<template>
  <v-card>
    <v-card-title>
      Tracks
    </v-card-title>
    <v-card-text>
      <v-data-table
        :headers="headers"
        :items="tracks"
        @click:row="itemSelected"
        :items-per-page="100"
        :loading="loading"
        :item-class="itemClasses">
        <template v-slot:item.color="{ item }">
          <span class="dot" :style="getStyle(item.hexColor)"></span>
        </template>
        <template v-slot:item.date="{ item }">
          {{formatDate(item.date)}}
        </template>
        <template v-slot:item.totalDistance="{ item }">
          {{formatDistance(item.totalDistance)}}
        </template>
        <template v-slot:item.totalElevation="{ item }">
          {{formatElevation(item.totalElevation)}}
        </template>
        <template v-slot:item.delete="{ item }">
          <v-btn icon @click="() => onRemove(item)">
            <v-icon>mdi-delete</v-icon>
          </v-btn>
        </template>
      </v-data-table>
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="onAdd">Add track</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    tracks: Array,
    loading: Boolean,
    selectedTrack: Object,
  },
  data: () => ({
    headers: [
      { text: '', value: 'color' },
      { text: 'Name', value: 'name' },
      { text: 'Ski area', value: 'skiArea.name' },
      { text: 'Date', value: 'date' },
      { text: 'Distance', value: 'totalDistance' },
      { text: 'Elevation', value: 'totalElevation' },
      { text: '', value: 'delete' },
    ],
  }),
  methods: {
    onAdd() {
      this.$emit('addTrack');
    },
    onRemove(track) {
      this.$emit('removeTrack', track);
    },
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
    getStyle(color) {
      return `background-color: ${color}`;
    },
    itemClasses(item) {
      if (this.isSelected(item)) return 'grey lighten-1';
      return '';
    },
  },
};
</script>

<style scoped>
.dot {
  width: 30px;
  height: 30px;
  border-radius: 15px;
  display: block;
}
</style>
