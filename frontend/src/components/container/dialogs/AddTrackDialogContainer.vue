<template>
  <add-track-dialog
    v-model="isOpen"
    @add="onAdd" />
</template>

<script>
import { ADD_TRACK, SET_DISPLAY_ADD_TRACK_DIALOG } from '../../../store/mutations';
import AddTrackDialog from '../../dialogs/AddTrackDialog.vue';

export default {
  components: { AddTrackDialog },
  data: () => ({
    isOpen: false,
  }),
  computed: {
    storeIsOpen() {
      return this.$store.state.showAddTrackDialog;
    },
  },
  watch: {
    storeIsOpen() {
      this.isOpen = this.storeIsOpen;
    },
    isOpen() {
      if (this.storeIsOpen !== this.isOpen) {
        this.$store.commit(SET_DISPLAY_ADD_TRACK_DIALOG, this.isOpen);
      }
    },
  },
  methods: {
    onAdd(name) {
      this.$store.commit(ADD_TRACK, {
        name,
      });
    },
  },
};
</script>
