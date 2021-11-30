<template>
  <v-card class="clickable" @click="tileClicked" v-if="selectedTrack">
    <v-card-title>
      Track stats
    </v-card-title>
    <v-card-text class="pa-0">
      <difficulty-pie-container :trackId="selectedTrack.id" height="250px" />
    </v-card-text>
  </v-card>
</template>

<script>
import DifficultyPieContainer from '../container/charts/DifficultyPieContainer.vue';

export default {
  components: { DifficultyPieContainer },
  computed: {
    selectedTrack() {
      return this.$store.getters.selectedTrack;
    },
  },
  methods: {
    tileClicked() {
      this.$router.push(`stats/${this.selectedTrack.id}`);
    },
  },
};

</script>

<style scoped>
.clickable {
  cursor: pointer;
}
</style>
