<template>
<div>
  <v-virtual-scroll
    :items="sortedPistes"
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
            {{item.reference}}
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </template>
  </v-virtual-scroll>
</div>
</template>

<script>
export default {
  props: {
    pistes: Array,
    selectedPiste: Object,
  },
  computed: {
    sortedPistes() {
      const copy = [...this.pistes];
      copy.sort((x, y) => this.sortByReference(x, y));
      return copy;
    },
  },
  methods: {
    itemSelected(piste) {
      this.$emit('pisteSelected', piste);
    },
    isSelected(piste) {
      return piste === this.selectedPiste;
    },
    parse(reference) {
      if (reference === null) return 99;
      const replaced = reference.replace(new RegExp('a|b|c|d|e'), reference);
      if (replaced.length === 0) return 99;
      const ref = parseInt(replaced, 10);
      if (Number.isNaN(ref)) return reference;
      return ref;
    },
    sortByReference(x, y) {
      const xRef = this.parse(x.reference);
      const yRef = this.parse(y.reference);
      if (xRef < yRef) { return -1; }
      if (xRef > yRef) { return 1; }
      return 0;
    },
  },
};
</script>
