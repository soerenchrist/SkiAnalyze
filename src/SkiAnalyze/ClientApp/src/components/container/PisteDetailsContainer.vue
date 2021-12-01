<template>
  <v-col v-if="isPisteSelected"
        :xs="12"
        :sm="12"
        :md="4"
        :lg="3"
        :xl="2"
        class="my-4 mr-4">
    <piste-details @close="onClose" :piste="selectedPiste" />
  </v-col>
</template>

<script>
import { SELECT_PISTE } from '../../store/actions';
import PisteDetails from '../PisteDetails.vue';

export default {
  components: {
    PisteDetails,
  },
  computed: {
    selectedPiste() {
      return this.$store.getters.selectedPiste;
    },
    isPisteSelected() {
      return this.selectedPiste !== null;
    },
  },
  methods: {
    onClose() {
      this.$store.dispatch(SELECT_PISTE, null);
    },
  },
};
</script>
