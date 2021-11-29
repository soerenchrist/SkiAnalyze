<template>
  <l-polyline
      v-if="isSelected"
      ref="polyline"
      :lat-lngs="latLngs"
      color="#ffff00"
      :weight="4"
      @click="polylineClicked">
  </l-polyline>
</template>

<script>
export default {
  props: {
    gondola: Object,
    color: String,
  },
  data: () => ({
    latLngs: [],
  }),
  computed: {
    isSelected() {
      if (this.$store.getters.selectedGondola === null) {
        if (this.$store.getters.selectedRun === null) return false;
        if (this.$store.getters.selectedRun.gondola === null) return false;
        return this.$store.getters.selectedRun.gondola.id === this.gondola.id;
      }
      return this.$store.getters.selectedGondola.id === this.gondola.id;
    },
  },
  methods: {
    polylineClicked() {
      this.$emit('onclick', this.gondola);
    },
  },
  mounted() {
    this.latLngs = [];
    this.gondola.coordinates.forEach((c) => {
      this.latLngs.push([c.latitude, c.longitude]);
    });
  },
};
</script>
