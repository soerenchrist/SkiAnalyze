<template>
  <pistes
    :pistes="pistes"
    :selectedPiste="selectedPiste"
    @pisteSelected="onPisteSelected"
    @difficultyFilterChanged="onDifficultyFilterChanged" />
</template>

<script>
import { SET_DIFFICULTY_FILTER, SET_SELECTED_PISTE } from '../../store/mutations';
import Pistes from '../Pistes.vue';

export default {
  components: {
    Pistes,
  },
  computed: {
    pistes() {
      return this.$store.getters.filteredPistes;
    },
    selectedPiste() {
      return this.$store.state.selectedPiste;
    },
  },
  methods: {
    onPisteSelected(piste) {
      this.$store.commit(SET_SELECTED_PISTE, piste);
    },
    onDifficultyFilterChanged(filter) {
      this.$store.commit(SET_DIFFICULTY_FILTER, filter);
    },
  },
};
</script>
