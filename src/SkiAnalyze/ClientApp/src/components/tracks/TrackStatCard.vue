<template>
  <v-card>
    <v-card-text>
      <v-list-item three-line>
        <v-list-item-content>
          <v-list-item-title class="text-h5 mb-1">
            {{value.toFixed(floatingPoints)}} {{unit}}
          </v-list-item-title>
          <v-list-item-subtitle>
            {{title}}
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  props: {
    title: String,
    value: Number,
    unit: String,
    floatingPoints: {
      default: 2,
      required: false,
      type: Number,
    },
  },
};
</script>
