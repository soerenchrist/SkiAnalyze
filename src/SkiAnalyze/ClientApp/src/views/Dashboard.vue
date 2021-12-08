<template>
  <div>
    <v-overlay :value="loading">
      <v-progress-circular
        indeterminate
        size="64" />
    </v-overlay>
    <totals-cards v-if="totals" :totals="totals" />
    <v-row>
      <v-col>
        <timeline />
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <last-tracks />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import LastTracks from '../components/dashboard/LastTracks.vue';
import Timeline from '../components/dashboard/Timeline.vue';
import TotalsCards from '../components/dashboard/TotalsCards.vue';
import DataService from '../services/DataService';

export default {
  components: { LastTracks, TotalsCards, Timeline },
  data: () => ({
    totals: null,
    loading: false,
  }),
  methods: {
    async getTotals() {
      this.totals = await DataService.getTotals();
    },
    async onMount() {
      this.loading = true;
      const promises = [];
      promises.push(this.getTotals());

      Promise.all(promises);
      this.loading = false;
    },
  },
  mounted() {
    this.onMount();
  },
};
</script>
