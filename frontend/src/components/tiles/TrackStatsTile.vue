<template>
  <v-card>
    <v-card-title>
      Track stats
    </v-card-title>
    <v-card-text class="pa-0">
      <difficulty-pie :seriesData="series" />
    </v-card-text>
  </v-card>
</template>

<script>
import DifficultyPie from '../charts/DifficultyPie.vue';
import DataService from '../../services/DataService';

export default {
  components: { DifficultyPie },
  data: () => ({
    series: [],
  }),
  computed: {
    selectedTrack() {
      return this.$store.getters.selectedTrack;
    },
  },
  watch: {
    selectedTrack() {
      if (this.selectedTrack !== null) {
        this.fetchStats(this.selectedTrack.id);
      }
    },
  },
  methods: {
    async fetchStats(trackId) {
      const stats = await DataService.getDifficultyStats(trackId);
      await this.buildSeries(stats);
    },
    buildSeries(stats) {
      const s = [];
      stats.forEach((stat) => {
        s.push({
          key: this.getName(stat.key),
          value: (stat.value * 100).toFixed(2),
        });
      });
      this.series = s;
    },
    getName(key) {
      switch (key) {
        case 0: return 'Easy';
        case 1: return 'Novice';
        case 2: return 'Intermediate';
        default: return 'Advanced';
      }
    },
  },
};

</script>
