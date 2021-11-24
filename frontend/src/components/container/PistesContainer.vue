<template>
  <pistes
    :pistes="pistes"
    :selectedPiste="selectedPiste"
    @pisteSelected="onPisteSelected" />
</template>

<script>
import { SET_SELECTED_PISTE } from '../../store/mutations';
import Pistes from '../Pistes.vue';

export default {
  components: {
    Pistes,
  },
  computed: {
    pistes() {
      return this.$store.state.pistes;
    },
    selectedPiste() {
      return this.$store.state.selectedPiste;
    },
  },
  methods: {
    onPisteSelected(piste) {
      this.$store.commit(SET_SELECTED_PISTE, piste);
    },
  },
};
</script>
