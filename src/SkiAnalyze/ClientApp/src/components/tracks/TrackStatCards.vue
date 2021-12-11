<template>
  <v-row>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        :title="$t('tracks.distance')"
        unit="km"
        :value="track.totalDistance/1000" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        :title="$t('tracks.elevation')"
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
        :title="$t('tracks.maxSpeed')"
        unit="km/h"
        :floatingPoints="1"
        :value="track.maxSpeed * 3.6" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        :title="$t('tracks.avgSpeed')"
        unit="km/h"
        :floatingPoints="1"
        :value="track.averageSpeed * 3.6" />
    </v-col>
    <v-col
      :xs="12"
      :sm="6"
      :md="4"
      :lg="2">
      <track-stat-card
        :title="$t('tracks.avgHeartRate')"
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
        :title="$t('tracks.maxHeartRate')"
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
