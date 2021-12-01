<template>
  <lifts
    :lifts="lifts"
    :selectedLift="selectedLift"
    @liftSelected="onLiftSelected" />
</template>

<script>
import { SELECT_GONDOLA } from '../../store/actions';
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
      return this.$store.getters.selectedGondola;
    },
  },
  methods: {
    onLiftSelected(lift) {
      this.$store.dispatch(SELECT_GONDOLA, lift);
    },
  },
};
</script>
