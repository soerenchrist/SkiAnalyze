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
    piste: Object,
  },
  data: () => ({
    keys: {
      reference: 'Referenz',
      lit: 'Beleuchtet',
      snowmaking: 'Beschneit',
      difficulty: 'Schwierigkeit',
    },
  }),
  computed: {
    title() {
      if (this.piste.name === null
        && this.piste.reference === null) return 'Piste';
      if (this.piste.name !== null) return `Piste ${this.piste.name}`;
      if (this.piste.reference !== null) return `Piste ${this.piste.reference}`;
      return '';
    },
    fields() {
      const values = [];
      for (const [key, value] of Object.entries(this.piste)) {
        if (value === null) continue;
        if (key === 'coordinates' || key === 'name') continue;
        if (key === 'difficulty') {
          values.push({
            key,
            name: this.parseKey(key),
            value: this.parseDifficulty(value),
          });
          continue;
        }
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
    parseDifficulty(value) {
      switch (value) {
        case 0: return 'Einfach';
        case 1: return 'Anfänger';
        case 2: return 'Fortgeschritten';
        default: return 'Schwer';
      }
    },
    parseValue(value) {
      if (value === false) return 'Nein';
      if (value === true) return 'Ja';
      return value;
    },
    onClose() {
      this.$emit('close');
    },
  },
};
</script>
