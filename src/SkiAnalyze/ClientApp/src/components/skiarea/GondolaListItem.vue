<template>
  <v-list-item
    @click="() => itemSelected()"
    :class="isSelected ? 'grey lighten-1' : ''">
    <v-list-item-content>
      <v-list-item-title v-if="gondola.name">
        {{gondola.name}}
      </v-list-item-title>
      <v-list-item-title v-if="!gondola.name">
        {{gondola.type}}
      </v-list-item-title>
      <v-list-item-subtitle v-if="gondola.reference">
        {{gondola.reference}}
      </v-list-item-subtitle>
    </v-list-item-content>
    <v-list-item-action v-if="displayDetailsButton">
      <v-btn icon @click="() => showDetails()">
        <v-icon>mdi-information</v-icon>
      </v-btn>
    </v-list-item-action>
  </v-list-item>
</template>

<script>
export default {
  props: {
    gondola: Object,
    isSelected: Boolean,
    displayDetailsButton: {
      type: Boolean,
      required: false,
      default: true,
    },
  },
  methods: {
    itemSelected() {
      this.$emit('click', this.gondola);
    },
    showDetails() {
      this.$emit('detailsClick', this.gondola);
    },
  },
};
</script>
