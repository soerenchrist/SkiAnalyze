<template>
<div>
  <v-virtual-scroll
    :items="sortedGondolas"
    :item-height="60"
    height="530">
    <template v-slot:default="{ item }">
      <gondola-list-item
        :gondola="item"
        :isSelected="isSelected(item)"
        @click="itemSelected"
        :displayUsageIndicator="displayUsageIndicator"
        :displayDetailsButton="displayDetailsButton"
        @detailsClick="showDetails">
      </gondola-list-item>
    </template>
  </v-virtual-scroll>
</div>
</template>

<script>
import GondolaListItem from './GondolaListItem.vue';

export default {
  props: {
    gondolas: Array,
    selectedGondola: Object,
    displayDetailsButton: {
      type: Boolean,
      required: false,
      default: true,
    },
    displayUsageIndicator: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  components: {
    GondolaListItem,
  },
  computed: {
    sortedGondolas() {
      const copy = [...this.gondolas];
      copy.sort((x, y) => {
        if (x.reference < y.reference) { return -1; }
        if (x.reference > y.reference) { return 1; }
        return 0;
      });
      return copy;
    },
  },
  methods: {
    itemSelected(gondola) {
      if (gondola === this.selectedGondola) {
        this.$emit('gondolaSelected', null);
      } else {
        this.$emit('gondolaSelected', gondola);
      }
    },
    showDetails(gondola) {
      this.$emit('showDetails', gondola);
    },
    isSelected(gondola) {
      return gondola === this.selectedGondola;
    },
  },
};
</script>
