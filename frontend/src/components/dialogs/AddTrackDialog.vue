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
          label="GPX file" />
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn text @click="onAdd">Add</v-btn>
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
  methods: {
    onAdd() {
      this.internalOpen = false;
      this.$emit('add', this.name);
    },
  },
};
</script>
