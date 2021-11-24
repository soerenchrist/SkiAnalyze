<template>
<div>
  <v-virtual-scroll
    :items="sortedLifts"
    :item-height="50"
    height="600"
  >
    <template v-slot:default="{ item }">
      <v-list-item
        @click="() => itemSelected(item)"
        :class="isSelected(item) ? 'grey lighten-1' : ''">
        <v-list-item-content>
          <v-list-item-title v-if="item.name">
            {{item.name}}
          </v-list-item-title>
          <v-list-item-title v-if="!item.name">
            {{item.type}}
          </v-list-item-title>
          <v-list-item-subtitle v-if="item.reference">
            {{item.reference}}
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </template>
  </v-virtual-scroll>
</div>
</template>

<script>
export default {
  props: {
    lifts: Array,
    selectedLift: Object,
  },
  computed: {
    sortedLifts() {
      const copy = [...this.lifts];
      copy.sort((x, y) => {
        if (x.reference < y.reference) { return -1; }
        if (x.reference > y.reference) { return 1; }
        return 0;
      });
      return copy;
    },
  },
  methods: {
    itemSelected(lift) {
      this.$emit('liftSelected', lift);
    },
    isSelected(lift) {
      return lift === this.selectedLift;
    },
  },
};
</script>
