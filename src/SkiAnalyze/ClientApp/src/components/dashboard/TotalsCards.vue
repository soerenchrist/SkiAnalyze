<template>
  <v-row>
    <v-col>
      <track-stat-card
        title="Total distance"
        :value="totals.totalDistance / 1000"
        unit="km"
        :floatingPoints="0" />
    </v-col>
    <v-col>
      <track-stat-card
        title="Total descent"
        :value="-1 * totals.totalElevation / 1000"
        unit="km"
        :floatingPoints="0" />
    </v-col>
    <v-col>
      <track-stat-card
        title="Maximum speed"
        unit="km/h"
        :value="totals.maxSpeed*3.6"
        :floatingPoints="1" />
    </v-col>
    <v-col>
      <track-stat-card
        title="Runs made"
        :value="totals.totalRuns"
        :floatingPoints="0" />
    </v-col>
    <v-col>
      <track-stat-card
        title="Gondolas used"
        :value="totals.totalGondolas"
        :floatingPoints="0" />
    </v-col>
    <v-col>
      <track-stat-card
        title="Tracks uploaded"
        :value="totals.totalTracks"
        :floatingPoints="0" />
    </v-col>
    <v-col v-if="totals.totalCalories > 0">
      <track-stat-card
        title="Calories burned"
        :value="totals.totalCalories"
        :floatingPoints="0" />
    </v-col>
  </v-row>
</template>

<script>
import TrackStatCard from '../tracks/TrackStatCard.vue';

export default {
  components: { TrackStatCard },
  props: {
    totals: Object,
  },
};
</script>
