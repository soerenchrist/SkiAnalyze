<template>
  <run-list
    :runs="runs"
    @runSelected="onRunSelected"
    :selectedRun="selectedRun"
    :isExpanded="isExpanded" />
</template>

<script>
import { SELECT_RUN } from '../../store/actions';
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
    selectedRun() {
      return this.$store.getters.selectedRun;
    },
    isExpanded() {
      return this.$store.getters.detailsExpanded;
    },
  },
  methods: {
    onRunSelected(run) {
      if (run == null) {
        this.$store.dispatch(SELECT_RUN, null);
        return;
      }

      this.$store.dispatch(SELECT_RUN, run);
    },
  },
};
</script>
