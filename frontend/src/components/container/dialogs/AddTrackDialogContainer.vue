<template>
  <add-track-dialog
    v-model="isOpen"
    @add="onAdd" />
</template>

<script>
import { ADD_TRACK } from '../../../store/actions';
import { SET_DISPLAY_ADD_TRACK_DIALOG } from '../../../store/mutations';
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
      const track = {
        name,
        color: '#ff0000',
        gpxFileContent: 'test',
      };
      this.$store.dispatch(ADD_TRACK, track);
    },
  },
};
</script>
