<template>
  <v-row>
    <v-col>
      <track-stat-card
        :value="gondolas.length"
        :floatingPoints="0"
        :title="$t('skiarea.numberOfGondolas')" />
    </v-col>
    <v-col>
      <track-stat-card
        :value="totalPisteLength"
        :floatingPoints="0"
        unit="km"
        :title="$t('skiarea.lengthOfPistes')" />
    </v-col>
    <v-col>
      <track-stat-card
        :value="percentageUsed"
        :floatingPoints="0"
        unit="%"
        :title="$t('skiarea.percentageGondolasUsed')" />
    </v-col>
    <v-col>
      <track-stat-card
        :value="totalGondolaLength"
        :floatingPoints="0"
        unit="km"
        :title="$t('skiarea.lengthOfGondolas')" />
    </v-col>
    <v-col>
      <v-card>
        <pie-chart
          :seriesData="series"
          height="120px"
          :tooltip="tooltip"
          :showLegend="false"
          :palettes="palette" />
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import PieChart from '../charts/PieChart.vue';
import TrackStatCard from '../tracks/TrackStatCard.vue';

export default {
  components: { TrackStatCard, PieChart },
  props: {
    gondolas: Array,
    pistes: Array,
  },
  data: () => ({
    series: [],
    palette: ['#3F51B5', '#2196F3', '#F44336', '#212121'],
    tooltip: { enable: true, format: '${point.x}: <b>${point.y} km</b>' },
  }),
  computed: {
    usedGondolas() {
      return this.gondolas.filter((x) => x.used);
    },
    totalPisteLength() {
      let total = 0;
      this.pistes.forEach((piste) => {
        total += piste.length;
      });
      return total / 1000;
    },
    percentageUsed() {
      const usedCount = this.usedGondolas.length;
      const allCount = this.gondolas.length;

      console.log(usedCount, allCount);

      if (allCount === 0) return 0;
      const result = usedCount / allCount;
      return result * 100;
    },
    totalGondolaLength() {
      let total = 0;
      this.gondolas.forEach((gondola) => {
        total += gondola.length;
      });
      return total / 1000;
    },
  },
  methods: {
    buildSeries() {
      const s = {
        Novice: { key: 'Novice', value: 0 },
        Easy: { key: 'Easy', value: 0 },
        Intermediate: { key: 'Intermediate', value: 0 },
        Advanced: { key: 'Advanced', value: 0 },
      };
      this.pistes.forEach((p) => {
        const name = this.getName(p.difficulty);
        s[name].value += p.length / 1000;
      });

      const keys = ['Novice', 'Easy', 'Intermediate', 'Advanced'];
      this.series = [];
      keys.forEach((k) => {
        const value = s[k];
        value.value = value.value.toFixed(2);
        this.series.push(value);
      });
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
  mounted() {
    this.buildSeries();
  },
};
</script>
