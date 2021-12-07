<template>
  <div>
    <run-data-table
      v-if="isExpanded && isBigScreen"
      :selectedRun="selectedRun"
      @runSelected="runSelected"
      :runs="runs" />
    <v-virtual-scroll
      v-else
      :items="runs"
      :item-height="60"
      height="485">
      <template v-slot:default="{ item }">
        <v-list-item
          @click="() => runSelected(item)"
          :class="isSelected(item) ? 'grey lighten-1' : ''">
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
          <v-list-item-action>
            <v-btn icon v-if="!item.downhill"
              @click="() => showGondolaInfo(item.gondola)">
              <v-icon>mdi-information-outline</v-icon>
            </v-btn>
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
    selectedRun: Object,
    isExpanded: Boolean,
  },
  methods: {
    runSelected(run) {
      if (this.isSelected(run)) this.$emit('runSelected', null);
      else this.$emit('runSelected', run);
    },
    getStyle(run) {
      return `background-color: ${run.color}`;
    },
    isSelected(run) {
      return run === this.selectedRun;
    },
    showGondolaInfo() {

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
