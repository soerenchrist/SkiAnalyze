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
    piste: Object,
    isSelected: Boolean,
  },
  computed: {
    color() {
      if (this.isSelected) return 'yellow';
      if (this.piste.difficulty <= 1) return 'blue';
      if (this.piste.difficulty === 2) return 'red';
      return 'black';
    },
    weight() {
      if (this.isSelected) return 3;
      return 1;
    },
  },
  data: () => ({
    latLngs: [],
  }),
  methods: {
    polylineClicked() {
      this.$emit('onclick', this.piste);
    },
  },
  mounted() {
    this.latLngs = [];
    this.piste.coordinates.forEach((c) => {
      this.latLngs.push([c.latitude, c.longitude]);
    });
  },
};
</script>
