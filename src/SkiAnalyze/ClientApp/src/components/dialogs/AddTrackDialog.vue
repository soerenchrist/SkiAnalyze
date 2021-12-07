<template>
  <v-dialog
    max-width="300"
    v-model="internalOpen">
    <v-card style="width: 300px">
      <v-card-title>
        Add track
      </v-card-title>
      <v-card-text>
        <v-text-field
          v-model="name"
          outlined
          dense
          label="Name" />
        <v-file-input
          outlined
          dense
          v-model="file"
          label=".gpx or .fit file" />
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
    file: null,
    name: '',
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
      return this.name.length > 0 && this.file !== null;
    },
  },
  methods: {
    async onAdd() {
      this.internalOpen = false;
      const track = {
        name: this.name,
        file: this.file,
        fileType: this.getFileType(this.file),
      };
      this.$emit('addTrack', track);
    },
    getFileType(file) {
      if (file.name.toLowerCase().endsWith('fit')) return 1;
      return 0;
    },
  },
};
</script>
