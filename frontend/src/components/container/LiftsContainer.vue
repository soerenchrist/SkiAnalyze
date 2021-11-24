<template>
  <lifts
    :lifts="lifts"
    :selectedLift="selectedLift"
    @liftSelected="onLiftSelected" />
</template>

<script>
import { SET_SELECTED_GONDOLA } from '../../store/mutations';
import Lifts from '../Lifts.vue';

export default {
  components: {
    Lifts,
  },
  computed: {
    lifts() {
      return this.$store.getters.gondolas;
    },
    selectedLift() {
      return this.$store.state.selectedGondola;
    },
  },
  methods: {
    onLiftSelected(lift) {
      this.$store.commit(SET_SELECTED_GONDOLA, lift);
    },
  },
};
</script>
