<template>
  <v-dialog
    max-width="300"
    v-model="internalOpen">
    <v-card style="width: 300px">
      <v-card-title>
        Add track
      </v-card-title>
      <v-card-text>
        <v-file-input
          outlined
          dense
          multiple
          v-model="files"
          label="Add one or more .gpx or .fit file" />
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn
          :disabled="!canSave"
          text
          @click="onAdd">Add</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    open: Boolean,
  },
  model: {
    prop: 'open',
    event: 'openChanged',
  },
  data: () => ({
    files: [],
    internalOpen: false,
  }),
  watch: {
    open() {
      this.internalOpen = this.open;
    },
    internalOpen() {
      this.$emit('openChanged', this.internalOpen);
    },
  },
  computed: {
    canSave() {
      return this.files.length > 0;
    },
  },
  methods: {
    async onAdd() {
      this.internalOpen = false;
      const tracks = [];
      this.files.forEach((file) => {
        const track = {
          file,
          fileType: this.getFileType(file),
        };
        tracks.push(track);
      });
      this.$emit('addTracks', tracks);
    },
    getFileType(file) {
      if (file.name.toLowerCase().endsWith('fit')) return 1;
      return 0;
    },
  },
};
</script>
