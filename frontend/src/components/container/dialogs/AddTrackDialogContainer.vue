<template>
  <add-track-dialog
    v-model="isOpen"
    @add="onAdd" />
</template>

<script>
import { ADD_TRACK } from '../../../store/actions';
import { DISPLAY_ADD_TRACK_DIALOG } from '../../../store/mutations';
import AddTrackDialog from '../../dialogs/AddTrackDialog.vue';

export default {
  components: { AddTrackDialog },
  data: () => ({
    isOpen: false,
  }),
  computed: {
    storeIsOpen() {
      return this.$store.getters.showAddTrackDialog;
    },
  },
  watch: {
    storeIsOpen() {
      this.isOpen = this.storeIsOpen;
    },
    isOpen() {
      if (this.storeIsOpen !== this.isOpen) {
        this.$store.commit(DISPLAY_ADD_TRACK_DIALOG, this.isOpen);
      }
    },
  },
  methods: {
    onAdd(track) {
      this.$store.dispatch(ADD_TRACK, track);
    },
  },
};
</script>
