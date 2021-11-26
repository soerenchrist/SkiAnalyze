<template>
  <v-virtual-scroll
    :items="runs"
    :item-height="60"
    height="600">
    <template v-slot:default="{ item }">
      <v-list>
        <v-list-item
          @click="() => runSelected(item)">
          <v-list-item-content>
            <v-list-item-title v-if="item.gondola !== null">
              {{item.gondola === null ? '' : item.gondola.name}}
            </v-list-item-title>
            <v-list-item-subtitle>
              {{item.number}}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
  </v-list>
    </template>
  </v-virtual-scroll>
</template>

<script>
import { SET_SELECTED_GONDOLA, SET_SELECTED_RUN } from '../../store/mutations';

export default {
  computed: {
    result() {
      return this.$store.getters.analysisResult;
    },
    hasResults() {
      return this.result !== null;
    },
    runs() {
      return this.result.runs;
    },
    gondolas() {
      return this.result.runs.filter((x) => !x.downhill);
    },
  },
  methods: {
    runSelected(run) {
      this.$store.commit(SET_SELECTED_RUN, run);
      if (run.gondola) {
        this.$store.commit(SET_SELECTED_GONDOLA, run.gondola);
      }
    },
  },
};
</script>
