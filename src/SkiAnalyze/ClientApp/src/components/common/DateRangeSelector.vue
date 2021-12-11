<template>
  <div class="d-flex justify-between align-center">
    <v-menu
      v-model="lowerRangeOpen"
      :close-on-content-click="false"
      :nudge-right="40"
      transition="scale-transition"
      offset-y
      min-width="auto">
      <template v-slot:activator="{ on, attrs }">
        <v-text-field
          v-model="lowerRange"
          :label="$t('common.from')"
          prepend-icon="mdi-calendar"
          v-bind="attrs"
          hide-details
          dense
          outlined
          v-on="on"
          readonly />
      </template>
      <v-date-picker
        v-model="lowerRange"
        :allowed-dates="allowedLowerRange"
        @input="lowerRangeOpen = false" />
    </v-menu>
    <span class="mx-3">-</span>
    <v-menu
      v-model="upperRangeOpen"
      :close-on-content-click="false"
      :nudge-right="40"
      transition="scale-transition"
      offset-y
      min-width="auto">
      <template v-slot:activator="{ on, attrs }">
        <v-text-field
          v-model="upperRange"
          :label="$t('common.to')"
          prepend-icon="mdi-calendar"
          v-bind="attrs"
          dense
          outlined
          hide-details
          v-on="on"
          readonly />
      </template>
      <v-date-picker
        v-model="upperRange"
        :allowed-dates="allowedUpperRange"
        @input="upperRangeOpen = false" />
    </v-menu>
  </div>
</template>

<script>
export default {
  data: () => ({
    lowerRangeOpen: false,
    upperRangeOpen: false,
    lowerRange: null,
    upperRange: null,
  }),
  watch: {
    lowerRange() {
      this.emitDate();
    },
    upperRange() {
      this.emitDate();
    },
  },
  methods: {
    allowedLowerRange(value) {
      if (this.upperRange === null) return true;
      const date = new Date(this.upperRange);
      const valDate = new Date(value);
      return valDate < date;
    },
    allowedUpperRange(value) {
      if (this.lowerRange === null) return true;
      const date = new Date(this.lowerRange);
      const valDate = new Date(value);
      return valDate > date;
    },
    formatDate(date) {
      return date.toISOString().substring(0, 10);
    },
    getLastYear() {
      const now = new Date();
      const lastYear = new Date();
      lastYear.setDate(now.getDate() - 365);
      return lastYear;
    },
    monthClick() {
      console.log('Test');
    },
    emitDate() {
      const lower = this.lowerRange === null ? this.getLastYear() : this.lowerRangeDate;
      const upper = this.upperRange === null ? new Date() : this.upperRangeDate;

      this.$emit('dateChanged', [lower, upper]);
    },
  },
  computed: {
    lowerRangeDate() {
      return new Date(this.lowerRange);
    },
    upperRangeDate() {
      return new Date(this.upperRange);
    },
  },
  mounted() {
    const now = new Date();
    const lastYear = this.getLastYear();
    this.lowerRange = this.formatDate(lastYear);
    this.upperRange = this.formatDate(now);
  },
};
</script>
