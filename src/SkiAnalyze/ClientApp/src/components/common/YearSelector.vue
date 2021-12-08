<template>
  <div class="d-flex justify-between">
    <v-btn icon small @click="dec">
      <v-icon>mdi-minus</v-icon>
    </v-btn>
    {{internalYear}}
    <v-btn icon small @click="inc" :disabled="incrementDisabled">
      <v-icon>mdi-plus</v-icon>
    </v-btn>
  </div>
</template>

<script>
export default {
  props: {
    year: Number,
    maxYear: {
      type: Number,
      required: false,
      default: -1,
    },
    minYear: {
      type: Number,
      required: false,
      default: -1,
    },
  },
  model: {
    prop: 'year',
    event: 'yearChanged',
  },
  data: () => ({
    internalYear: 0,
  }),
  watch: {
    internalYear() {
      this.$emit('yearChanged', this.internalYear);
    },
    year() {
      if (this.internalYear !== this.year) {
        this.internalYear = this.year;
      }
    },
  },
  computed: {
    incrementDisabled() {
      if (this.maxYear === -1) return false;
      return this.maxYear === this.internalYear;
    },
    decrementDisabled() {
      if (this.minYear === -1) return false;
      return this.minYear === this.internalYear;
    },
  },
  methods: {
    dec() {
      this.internalYear--;
    },
    inc() {
      this.internalYear++;
    },
  },
  mounted() {
    this.internalYear = this.year;
  },
};
</script>
