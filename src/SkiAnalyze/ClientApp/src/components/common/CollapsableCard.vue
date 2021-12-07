<template>
  <v-card>
    <v-card-title>
      {{title}}
      <v-spacer />
      <v-btn icon @click="toggleExpand">
        <v-icon>{{icon}}</v-icon>
      </v-btn>
    </v-card-title>
    <v-expand-transition>
      <v-card-text
        v-if="isExpanded"
        :class="classes">
          <slot />
      </v-card-text>
    </v-expand-transition>
  </v-card>
</template>

<script>
export default {
  props: {
    title: String,
    textClass: String,
  },
  data: () => ({
    isExpanded: true,
  }),
  computed: {
    icon() {
      return this.isExpanded ? 'mdi-chevron-up' : 'mdi-chevron-down';
    },
    classes() {
      return `transition-fast-in-fast-out v-card--reveal ${this.textClass}`;
    },
  },
  methods: {
    toggleExpand() {
      this.isExpanded = !this.isExpanded;
    },
  },
};
</script>
