<template>
  <v-col v-if="isGondolaSelected"
        :xs="12"
        :sm="12"
        :md="4"
        :lg="3"
        :xl="2"
        class="my-4 mr-4">
    <gondola-details :gondola="selectedGondola" @close="onClose" />
  </v-col>
</template>

<script>
import { SET_SELECTED_GONDOLA } from '../../store/mutations';
import GondolaDetails from '../GondolaDetails.vue';

export default {
  components: {
    GondolaDetails,
  },
  computed: {
    selectedGondola() {
      return this.$store.state.selectedGondola;
    },
    isGondolaSelected() {
      return this.selectedGondola !== null;
    },
  },
  methods: {
    onClose() {
      this.$store.commit(SET_SELECTED_GONDOLA, null);
    },
  },
};
</script>
