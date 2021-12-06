<template>
  <v-dialog
    max-width="300"
    v-model="internalOpen">
    <v-card>
      <v-card-title>
        Add track
      </v-card-title>
      <v-card-text>
        <v-text-field
          label="Name"
          outlined
          dense
          v-model="name" />
        <v-file-input
          outlined
          dense
          v-model="file"
          label="GPX file" />
        <v-color-picker v-model="color"></v-color-picker>
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
    name: '',
    file: null,
    color: '#ff0000',
    internalOpen: false,
  }),
  watch: {
    open() {
      this.internalOpen = this.open;
      if (this.open) {
        this.color = this.getRandomColor();
      }
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
        color: this.color,
      };
      this.$emit('add', track);
    },
    getFileType(file) {
      console.log(file);
      if (file.name.toLowerCase().endsWith('fit')) return 1;
      return 0;
    },
    getRandomColor() {
      return `#${Math.floor(Math.random() * 16777215).toString(16)}`;
    },
  },
};
</script>
