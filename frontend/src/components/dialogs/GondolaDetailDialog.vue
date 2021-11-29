<template>
  <v-dialog
    max-width="300"
    width="250"
    v-model="internalOpen">
    <v-card>
      <v-card-title>
        {{gondola.name}}
      </v-card-title>
      <v-card-text>
        <gondola-details :gondola="gondola" />
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { SET_SHOW_GONDOLA_DETAILS_DIALOG } from '../../store/mutations';
import GondolaDetails from '../GondolaDetails.vue';

export default {
  components: { GondolaDetails },
  data: () => ({
    internalOpen: false,
  }),
  computed: {
    isOpen() {
      return this.$store.getters.showGondolaDetailsDialog;
    },
    gondola() {
      return this.$store.getters.selectedGondola;
    },
  },
  watch: {
    internalOpen() {
      if (this.internalOpen !== this.isOpen) {
        this.$store.commit(SET_SHOW_GONDOLA_DETAILS_DIALOG, this.internalOpen);
      }
    },
    isOpen() {
      if (this.internalOpen !== this.isOpen) {
        this.internalOpen = this.isOpen;
      }
    },
  },
};
</script>
