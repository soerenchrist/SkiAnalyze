<template>
  <v-card>
    <v-card-title>
      {{title}}
      <v-spacer />
      <slot name="headerButtons" />

      <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-btn icon @click="toggleExpand"
          v-bind="attrs"
          v-on="on">
          <v-icon>{{icon}}</v-icon>
        </v-btn>
      </template>
      <span>{{tooltipText}}</span>
    </v-tooltip>

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
    tooltipText() {
      return this.isExpanded ? 'Collapse card' : 'Expand card';
    },
  },
  methods: {
    toggleExpand() {
      this.isExpanded = !this.isExpanded;
    },
  },
};
</script>
