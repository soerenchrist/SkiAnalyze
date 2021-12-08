<template>
  <div>
    <v-btn
      v-for="item in items"
      :key="(`${item.id}_${item.name}`)"
      small
      class="mr-1"
      @click="() => itemClicked(item)"
      :color="getClass(item)">
      {{item.name}}
    </v-btn>
  </div>
</template>

<script>
export default {
  model: {
    event: 'selectedItemChanged',
    prop: 'selectedItem',
  },
  props: {
    selectedItem: Number,
    items: {
      type: Array,
      required: true,
    },
  },
  methods: {
    itemClicked(item) {
      this.$emit('selectedItemChanged', item.id);
    },
    getClass(item) {
      if (item.id === this.selectedItem) {
        return 'primary';
      }
      return 'secondary';
    },
  },
};
</script>
