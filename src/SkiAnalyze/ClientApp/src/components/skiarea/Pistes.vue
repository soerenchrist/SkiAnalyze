<template>
<div>
  <v-virtual-scroll
    :items="consolidatedPistes"
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
            {{item.reference}}
          </v-list-item-title>
          <v-list-item-subtitle v-if="item.name">
            {{item.reference}}
          </v-list-item-subtitle>
        </v-list-item-content>
        <v-list-item-action>
          <div class="d-flex">
            <div
              v-for="diff in item.difficulties"
              :key="diff"
              class="mx-1"
              :class="getColorClass(diff)"></div>
          </div>
        </v-list-item-action>
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
  data: () => ({
    consolidatedPistes: [],
  }),
  computed: {
    sortedPistes() {
      const copy = [...this.pistes];
      copy.sort((x, y) => this.sortByReference(x, y));
      return copy;
    },
  },
  methods: {
    itemSelected(piste) {
      if (piste === this.selectedPiste) {
        this.$emit('pisteSelected', null);
      } else {
        this.$emit('pisteSelected', piste);
      }
    },
    isSelected(piste) {
      return piste === this.selectedPiste;
    },
    parse(reference) {
      if (reference === null) return 99;
      const replaced = reference.replace(new RegExp('a|b|c|d|e'), '');
      if (replaced.length === 0) return 99;
      const ref = parseInt(replaced, 10);
      if (Number.isNaN(ref)) return reference;
      return ref;
    },
    getColorClass(difficulty) {
      if (difficulty <= 1) return 'dot blue';
      if (difficulty === 2) return 'dot red';
      return 'dot black';
    },
    consolidatePistes(pistes) {
      const consolidated = {};

      pistes.forEach((piste) => {
        if (piste.reference in consolidated) {
          const p = consolidated[piste.reference];
          if (!p.difficulties.includes(piste.difficulty)) {
            p.difficulties.push(piste.difficulty);
            p.originals.push(piste);
          }
        } else {
          consolidated[piste.reference] = {
            name: piste.name,
            reference: piste.reference,
            originals: [piste],
            numericId: this.parse(piste.reference),
            difficulties: [piste.difficulty],
          };
        }
      });

      const results = [];
      // eslint-disable-next-line no-unused-vars
      const keys = Object.keys(consolidated);

      keys.forEach((key) => {
        results.push(consolidated[key]);
      });
      return results;
    },
  },
  mounted() {
    const consolidated = this.consolidatePistes(this.pistes);
    consolidated.sort((a, b) => a.numericId - b.numericId);
    this.consolidatedPistes = consolidated;
  },
};
</script>

<style scoped>
.dot {
  width: 20px;
  height: 20px;
  border-radius: 15px;
}
</style>
