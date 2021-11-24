<template>
<v-card>
  <v-card-title>
    {{title}}
  </v-card-title>
  <v-card-text>
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
  </v-card-text>
  <v-card-actions>
    <v-btn text @click="onClose">Details schließen</v-btn>
  </v-card-actions>
</v-card>
</template>

<script>
export default {
  props: {
    gondola: Object,
  },
  data: () => ({
    keys: {
      reference: 'Referenz',
      occupancy: 'Personen',
      capacity: 'Kapazität',
      oneWay: 'Nur in eine Richtung befahrbar',
      heating: 'Beheizt',
      type: 'Typ',
      name: 'Name',
      bubble: 'Haube',
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
        if (key === 'coordinates') continue;
        values.push({
          key,
          name: this.parseKey(key),
          value: this.parseValue(value),
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
    parseValue(value) {
      if (value === false) return 'Nein';
      if (value === true) return 'Ja';
      if (value === 'gondola') return 'Gondel';
      if (value === 'chair_lift') return 'Sessellift';
      return value;
    },
  },
};
</script>
