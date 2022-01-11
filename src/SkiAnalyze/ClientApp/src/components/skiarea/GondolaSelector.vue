<template>
  <v-card>
    <v-card-title>
      {{$t('gondola.select')}}
    </v-card-title>
    <v-card-text>
      <v-progress-circular v-if="loading" />
      <gondolas
        v-else
        @gondolaSelected="gondolaSelected"
        :displayDetailsButton="false"
        :gondolas="gondolas" />
    </v-card-text>
  </v-card>
</template>

<script>
import DataService from '../../services/DataService';
import Gondolas from './Gondolas.vue';

export default {
  components: { Gondolas },
  props: {
    skiAreaId: Number,
  },
  data() {
    return {
      loading: true,
      gondolas: [],
    };
  },
  methods: {
    async fetchGondolas() {
      this.gondolas = await DataService.getGondolasForSkiArea(this.skiAreaId);
      this.loading = false;
    },
    gondolaSelected(gondola) {
      this.$emit('gondolaSelected', gondola);
    },
  },
  mounted() {
    this.fetchGondolas();
  },
};
</script>
