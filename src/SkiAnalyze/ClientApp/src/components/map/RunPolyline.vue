<template>
<div>
  <l-polyline
      :lat-lngs="latLngs"
      :color="color"
      :weight="weight">
  </l-polyline>
  <l-marker
    @click="runSelected"
    v-if="markerPosition && showRunNumber"
    :lat-lng="markerPosition">
    <l-icon
          :icon-anchor="customIconsAnchor"
          class-name="iconClass">
      <div>{{ run.number }}</div>
    </l-icon>
  </l-marker>
</div>
</template>

<script>
export default {
  props: {
    run: Object,
    showRunNumber: Boolean,
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
      return 0;
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
    markerPosition: null,
    customIconsAnchor: [12, 12],
  }),
  methods: {
    setRunMarkerPosition() {
      const middleIndex = Math.floor(this.latLngs.length / 2);

      const lowerRange = Math.max(middleIndex - 2, 0);
      const upperRange = Math.min(middleIndex + 3, 0);

      const randomPos = this.getRandomInRange(lowerRange, upperRange);
      this.markerPosition = this.latLngs[randomPos];
    },
    runSelected() {
      this.$emit('runSelected', this.run);
    },
    getRandomInRange(min, max) {
      return Math.floor(Math.random() * (max - min) + min);
    },
  },
  mounted() {
    this.latLngs = [];
    this.run.coordinates.forEach((c) => {
      this.latLngs.push([c.latitude, c.longitude]);
    });
    this.setRunMarkerPosition();
  },
};
</script>

<style>
.iconClass {
  background-color: white;
  border-radius: 13px;
  width: 25px !important;
  height: 25px !important;
  text-align: center;
  display: block;
}
</style>
