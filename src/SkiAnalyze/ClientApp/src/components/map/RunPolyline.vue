<template>
  <l-polyline
      :lat-lngs="latLngs"
      :color="color"
      :weight="weight">
  </l-polyline>
</template>

<script>
export default {
  props: {
    run: Object,
    selectedRun: Object,
  },
  computed: {
    isSelected() {
      if (this.selectedRun === null) return false;
      return this.selectedRun.id === this.run.id;
    },
    weight() {
      if (this.isSelected) {
        return 8;
      }
      return 3;
    },
    color() {
      if (this.isSelected) {
        return 'yellow';
      }
      return 'cyan';
    },
  },
  data: () => ({
    latLngs: [],
  }),
  mounted() {
    this.latLngs = [];
    this.run.coordinates.forEach((c) => {
      this.latLngs.push([c.latitude, c.longitude]);
    });
  },
};
</script>
