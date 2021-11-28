<template>
  <v-col
    :xs="12"
    :sm="12"
    :md="6"
    :lg="getClass('lg')"
    :xl="getClass('xl')"
    class="mt-4 mx-4">
    <v-card>
      <v-toolbar
        dark
        color="primary"
        dense>

        <v-spacer></v-spacer>

        <v-btn icon @click="expandDialog" class="d-none d-lg-block">
          <v-icon>{{ isExpanded ? 'mdi-arrow-collapse' : 'mdi-arrow-expand'}}</v-icon>
        </v-btn>

        <template v-slot:extension>
          <v-tabs
            dark
            v-model="selectedTab"
            show-arrows
          >
            <v-tabs-slider color="primary lighten-3"></v-tabs-slider>

            <v-tab key="tracks">Tracks</v-tab>
            <v-tab key="lifts">Lifts</v-tab>
            <v-tab key="pistes">Pistes</v-tab>
            <v-tab key="runList" v-if="isAnalyzed">Runs</v-tab>
          </v-tabs>
        </template>
      </v-toolbar>

      <v-tabs-items v-model="selectedTab">

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
          <run-list-container />
        </v-tab-item>
      </v-tabs-items>
    </v-card>
  </v-col>
</template>

<script>
import { TOGGLE_EXPAND_DETAILS } from '../store/mutations';
import LiftsContainer from './container/LiftsContainer.vue';
import PistesContainer from './container/PistesContainer.vue';
import RunListContainer from './container/RunListContainer.vue';
import TracksContainer from './container/TracksContainer.vue';

export default {
  components: {
    TracksContainer,
    LiftsContainer,
    PistesContainer,
    RunListContainer,
  },
  data: () => ({
    selectedTab: 0,
  }),
  methods: {
    expandDialog() {
      this.$store.commit(TOGGLE_EXPAND_DETAILS);
    },
    getClass(size) {
      if (size === 'xl') {
        return this.isExpanded ? 8 : 3;
      }
      return this.isExpanded ? 8 : 4;
    },
  },
  computed: {
    isAnalyzed() {
      return this.$store.getters.analysisResult !== null;
    },
    isExpanded() {
      return this.$store.getters.detailsExpanded;
    },
  },
};
</script>
