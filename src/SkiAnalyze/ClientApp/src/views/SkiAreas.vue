<template>
  <ski-area-map @showDetails="onShowDetails" />
</template>

<script>
import SkiAreaMap from '../components/map/SkiAreaMap.vue';

export default {
  components: { SkiAreaMap },
  methods: {
    onShowDetails(area) {
      this.$router.push({ name: 'SkiArea', params: { skiAreaId: area.id } });
    },
  },
};
</script>
