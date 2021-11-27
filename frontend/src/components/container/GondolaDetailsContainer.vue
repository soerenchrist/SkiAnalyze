<template>
  <v-col v-if="isGondolaSelected"
        :xs="12"
        :sm="12"
        :md="3"
        :lg="2"
        :xl="2"
        class="my-4 mr-4">
    <gondola-details :gondola="selectedGondola" @close="onClose" />
  </v-col>
</template>

<script>
import { SELECT_GONDOLA } from '../../store/actions';
import GondolaDetails from '../GondolaDetails.vue';

export default {
  components: {
    GondolaDetails,
  },
  computed: {
    selectedGondola() {
      return this.$store.getters.selectedGondola;
    },
    isGondolaSelected() {
      return this.selectedGondola !== null;
    },
  },
  methods: {
    onClose() {
      this.$store.dispatch(SELECT_GONDOLA, null);
    },
  },
};
</script>
