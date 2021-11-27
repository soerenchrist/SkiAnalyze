<template>
  <run-list
    :runs="runs"
    @runSelected="onRunSelected" />
</template>

<script>
import { SELECT_GONDOLA, SELECT_RUN } from '../../store/actions';
import RunList from '../analysis/RunList.vue';

export default {
  components: { RunList },
  computed: {
    result() {
      return this.$store.getters.analysisResult;
    },
    runs() {
      return this.result.runs;
    },
  },
  methods: {
    onRunSelected(run) {
      if (run.gondola) {
        this.$store.dispatch(SELECT_GONDOLA, run.gondola);
      } else {
        this.$store.dispatch(SELECT_RUN, run);
      }
    },
  },
};
</script>
