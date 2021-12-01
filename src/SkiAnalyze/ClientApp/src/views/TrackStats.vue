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
            <v-spacer />
            <button-bar v-model="selectedProperty" :items="propertyItems" />
          </v-card-title>
          <v-card-text class="pa-0">
            <gondola-types-pie-container
              :showLegend="true"
              :propertyName="selectedPropertyName"
              :trackId="numericTrackId" />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import ButtonBar from '../components/common/ButtonBar.vue';
import DifficultyPieContainer from '../components/container/charts/DifficultyPieContainer.vue';
import GondolaTypesPieContainer from '../components/container/charts/GondolaTypesPieContainer.vue';

export default {
  props: {
    trackId: String,
  },
  components: { DifficultyPieContainer, GondolaTypesPieContainer, ButtonBar },
  data: () => ({
    propertyItems: [
      { id: 1, name: 'Type', key: 'type' },
      { id: 2, name: 'Heating', key: 'heating' },
      { id: 3, name: 'Occupancy', key: 'occupancy' },
    ],
    selectedProperty: 1,
  }),
  computed: {
    numericTrackId() {
      return parseInt(this.trackId, 10);
    },
    selectedPropertyName() {
      return this.propertyItems.filter((x) => x.id === this.selectedProperty)[0].key;
    },
  },
};
</script>
