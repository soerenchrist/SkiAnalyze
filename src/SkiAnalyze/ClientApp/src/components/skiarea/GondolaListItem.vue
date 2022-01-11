<template>
  <v-list-item
    @click="() => itemSelected()"
    :class="isSelected ? 'grey lighten-1' : ''">
    <v-list-item-icon v-if="displayUsageIndicator">
      <v-tooltip bottom v-if="gondola.used">
        <template v-slot:activator="{ on, attrs }">
          <v-icon
            color="green"
            v-bind="attrs"
            v-on="on">
            mdi-check
          </v-icon>
        </template>
        <span>{{$t('gondola.used')}}</span>
      </v-tooltip>
      <v-tooltip bottom v-else>
        <template v-slot:activator="{ on, attrs }">
          <v-icon
            color="red"
            v-bind="attrs"
            v-on="on">
            mdi-close
          </v-icon>
        </template>
        <span>{{$t('gondola.notused')}}</span>
      </v-tooltip>
    </v-list-item-icon>
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
    displayUsageIndicator: {
      type: Boolean,
      required: false,
      default: false,
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
