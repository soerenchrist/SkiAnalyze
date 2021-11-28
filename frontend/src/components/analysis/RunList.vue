<template>
  <div>
    <run-data-table v-if="isExpanded && isBigScreen" @runSelected="runSelected" :runs="runs" />
    <v-virtual-scroll
      v-else
      :items="runs"
      :item-height="60"
      height="600">
      <template v-slot:default="{ item }">
        <v-list-item
          @click="() => runSelected(item)">
          <v-list-item-icon>
            <v-icon>{{item.downhill ? 'mdi-ski' : 'mdi-gondola'}}</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-if="item.downhill">
              Descent {{item.number}}
            </v-list-item-title>
            <v-list-item-title v-else>
              {{item.gondola.name}}
            </v-list-item-title>
          </v-list-item-content>
          <v-list-item-action v-if="item.downhill">
            <span class="dot" :style="getStyle(item)"></span>
          </v-list-item-action>
        </v-list-item>
      </template>
    </v-virtual-scroll>
  </div>
</template>

<script>
import RunDataTable from './RunDataTable.vue';

export default {
  components: { RunDataTable },
  props: {
    runs: Array,
    isExpanded: Boolean,
  },
  methods: {
    runSelected(run) {
      this.$emit('runSelected', run);
    },
    getStyle(run) {
      return `background-color: ${run.color}`;
    },
  },
  computed: {
    isBigScreen() {
      const breakpoint = this.$vuetify.breakpoint.name;
      return breakpoint === 'lg' || breakpoint === 'xl';
    },
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
