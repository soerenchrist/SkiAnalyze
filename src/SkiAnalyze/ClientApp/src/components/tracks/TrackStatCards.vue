<template>
  <v-row>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Distance"
        unit="km"
        :value="track.totalDistance/1000" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Descent"
        unit="m"
        :floatingPoints="0"
        :value="-1 * track.totalElevation" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Max speed"
        unit="km/h"
        :floatingPoints="1"
        :value="track.maxSpeed" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Average speed"
        unit="km/h"
        :floatingPoints="1"
        :value="track.averageSpeed" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Average Heart rate"
        unit="bpm"
        :floatingPoints="0"
        :value="track.averageHeartRate" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        title="Max Heart rate"
        unit="bpm"
        :floatingPoints="0"
        :value="track.maxHeartRate" />
    </v-col>
  </v-row>
</template>

<script>
import TrackStatCard from './TrackStatCard.vue';

export default {
  components: { TrackStatCard },
  props: {
    track: Object,
  },
};
</script>
