<template>
  <v-btn
    v-if="canAnalyze"
    primary
    @click="analyze">Analyze</v-btn>
</template>

<script>
import { START_ANALYSIS } from '../../store/actions';

export default {
  computed: {
    canAnalyze() {
      return this.$store.getters.tracks.length > 0;
    },
  },
  methods: {
    analyze() {
      this.$store.dispatch(START_ANALYSIS);
    },
  },
};
</script>
