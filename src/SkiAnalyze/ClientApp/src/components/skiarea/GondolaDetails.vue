<template>
<div>
  <v-list-item v-for="item in fields" :key="item.key">
    <v-list-item-content>
      <v-list-item-title>
        {{item.value}}
      </v-list-item-title>
      <v-list-item-subtitle>
        {{item.name}}
      </v-list-item-subtitle>
    </v-list-item-content>
  </v-list-item>
</div>
</template>

<script>
export default {
  props: {
    gondola: Object,
  },
  data: () => ({
    keys: {
      reference: 'Reference',
      occupancy: 'Occupancy',
      capacity: 'Capacity',
      oneWay: 'One way',
      heating: 'Heating',
      type: 'Type',
      name: 'Name',
      bubble: 'Bubble',
      duration: 'Duration',
      length: 'Length',
      used: 'Used',
    },
  }),
  computed: {
    title() {
      if (this.gondola.name === null) return 'Lift';
      return this.gondola.name;
    },
    fields() {
      const values = [];
      for (const [key, value] of Object.entries(this.gondola)) {
        if (value === null) continue;
        if (key === 'coordinates'
        || key === 'id') continue;
        values.push({
          key,
          name: this.parseKey(key),
          value: this.parseValue(key, value),
        });
      }
      return values;
    },
  },
  methods: {
    parseKey(key) {
      if (key in this.keys) {
        return this.keys[key];
      }
      return key;
    },
    onClose() {
      this.$emit('close');
    },
    parseValue(key, value) {
      if (key === 'length') {
        return `${value.toFixed(0)} m`;
      }
      if (value === false) return 'No';
      if (value === true) return 'Yes';
      if (value === 'gondola') return 'Gondola';
      if (value === 'chair_lift') return 'Chair lift';
      return value;
    },
  },
};
</script>
