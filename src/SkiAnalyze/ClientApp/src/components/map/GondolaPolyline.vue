<template>
  <l-polyline
    v-if="gondola"
    :lat-lngs="latLngs"
    color="yellow">
    <l-tooltip :options="{ permanent: true, interactive: true }">
      <b>{{gondola.name}}</b><br />
      <span>Type: {{convertType(gondola.type)}}</span> <br />
      <span v-if="gondola.heating !== null">
        Heating: {{convertBoolToYesNo(gondola.heating)}}
      </span> <br />
      <span v-if="gondola.occupancy !== null">Occupancy: {{gondola.occupancy}}</span>
    </l-tooltip>
  </l-polyline>
</template>

<script>
export default {
  props: {
    gondola: Object,
  },
  computed: {
    latLngs() {
      if (!this.gondola) return [];
      return this.gondola.coordinates.map((x) => [x.latitude, x.longitude]);
    },
  },
  methods: {
    convertBoolToYesNo(b) {
      return b ? 'yes' : 'no';
    },
    convertType(type) {
      switch (type) {
        case 'gondola':
          return 'gondola';
        case 'cable_car':
          return 'cable car';
        case 't-bar':
          return 't-bar';
        case 'magic_carpet':
          return 'magic carpet';
        case 'drag_lift':
          return 'drag lift';
        default:
          return 'chair lift';
      }
    },
  },
};
</script>
