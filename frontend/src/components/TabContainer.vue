<template>
  <v-col
    :xs="12"
    :sm="12"
    :md="4"
    :lg="3"
    :xl="2"
    class="mt-4 ml-4">
    <v-card>
      <v-tabs
        dark
        background-color="primary darken-3"
        show-arrows
      >
        <v-tabs-slider color="primary lighten-3"></v-tabs-slider>

        <v-tab key="tracks">Tracks</v-tab>
        <v-tab key="lifts">Lifts</v-tab>
        <v-tab key="pistes">Pistes</v-tab>
        <v-tab key="runList" v-if="isAnalyzed">Runs</v-tab>

        <v-tab-item>
          <tracks-container />
        </v-tab-item>
        <v-tab-item>
          <lifts-container />
        </v-tab-item>
        <v-tab-item>
          <pistes-container />
        </v-tab-item>
        <v-tab-item v-if="isAnalyzed">
          <run-list />
        </v-tab-item>

      </v-tabs>
    </v-card>
  </v-col>
</template>

<script>
import LiftsContainer from './container/LiftsContainer.vue';
import PistesContainer from './container/PistesContainer.vue';
import TracksContainer from './container/TracksContainer.vue';
import RunList from './other/RunList.vue';

export default {
  components: {
    TracksContainer,
    LiftsContainer,
    PistesContainer,
    RunList,
  },
  computed: {
    isAnalyzed() {
      return this.$store.getters.analysisResult !== null;
    },
  },
};
</script>
