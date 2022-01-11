<template>
  <v-dialog
    max-width="500"
    width="500"
    v-model="internalOpen">
    <gondola-selector
      @gondolaSelected="gondolaSelected"
      :skiAreaId="skiAreaId" />
  </v-dialog>
</template>

<script>
import GondolaSelector from '../skiarea/GondolaSelector.vue';

export default {
  components: { GondolaSelector },
  props: {
    open: Boolean,
    skiAreaId: Number,
  },
  model: {
    prop: 'open',
    event: 'openChanged',
  },
  data: () => ({
    internalOpen: false,
  }),
  methods: {
    gondolaSelected(gondola) {
      this.$emit('gondolaSelected', gondola);
    },
  },
  watch: {
    open() {
      this.internalOpen = this.open;
    },
    internalOpen() {
      this.$emit('openChanged', this.internalOpen);
    },
  },
};
</script>
