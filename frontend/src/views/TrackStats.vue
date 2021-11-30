<template>
  <div>
    <v-row>
      <v-col
        :sm="12"
        :md="6"
        :lg="4"
        :xl="3">
        <v-card class="ma-4">
          <v-card-title>
            Pistes by difficulty
          </v-card-title>
          <v-card-text class="pa-0">
            <difficulty-pie-container :showLegend="true" :trackId="numericTrackId" />
          </v-card-text>
        </v-card>
      </v-col>
      <v-col
        :sm="12"
        :md="6"
        :lg="4"
        :xl="3">
        <v-card class="ma-4">
          <v-card-title>
            Gondolas by type
          </v-card-title>
          <v-card-text class="pa-0">
            <gondola-types-pie-container :showLegend="true" :trackId="numericTrackId" />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import DifficultyPieContainer from '../components/container/charts/DifficultyPieContainer.vue';
import GondolaTypesPieContainer from '../components/container/charts/GondolaTypesPieContainer.vue';

export default {
  props: {
    trackId: String,
  },
  components: { DifficultyPieContainer, GondolaTypesPieContainer },
  computed: {
    numericTrackId() {
      return parseInt(this.trackId, 10);
    },
  },
};
</script>
