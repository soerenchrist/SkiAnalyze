<template>
<div>
  <v-virtual-scroll
    :items="sortedGondolas"
    :item-height="50"
    height="530"
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
    gondolas: Array,
    selectedGondola: Object,
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
    isSelected(gondola) {
      return gondola === this.selectedGondola;
    },
  },
};
</script>
