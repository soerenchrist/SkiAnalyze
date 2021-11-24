<template>
  <l-polyline
      ref="polyline"
      :lat-lngs="latLngs"
      :color="color"
      :weight="weight"
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
      return this.$store.state.selectedGondola === this.gondola;
    },
    weight() {
      if (this.isSelected) return 8;
      return 3;
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
