<template>
  <v-overlay :value="loading">
    <v-progress-circular
      indeterminate
      size="64" />
  </v-overlay>
</template>

<script>
export default {
  props: {
    loading: Boolean,
  },
};
</script>
