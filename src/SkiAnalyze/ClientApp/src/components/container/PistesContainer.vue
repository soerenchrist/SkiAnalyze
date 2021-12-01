<template>
  <pistes
    :pistes="pistes"
    :selectedPiste="selectedPiste"
    @pisteSelected="onPisteSelected"
    @difficultyFilterChanged="onDifficultyFilterChanged" />
</template>

<script>
import { SELECT_PISTE } from '../../store/actions';
import { SET_DIFFICULTY_FILTER } from '../../store/mutations';
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
      return this.$store.getters.selectedPiste;
    },
  },
  methods: {
    onPisteSelected(piste) {
      this.$store.dispatch(SELECT_PISTE, piste);
    },
    onDifficultyFilterChanged(filter) {
      this.$store.commit(SET_DIFFICULTY_FILTER, filter);
    },
  },
};
</script>
