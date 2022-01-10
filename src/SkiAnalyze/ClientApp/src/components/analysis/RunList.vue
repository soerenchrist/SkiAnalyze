<template>
  <div>
    <run-data-table
      v-if="isExpanded"
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
              {{$t('tracks.run')}} {{item.number}}
            </v-list-item-title>
            <v-list-item-title v-else>
              {{item.gondola.name}}
            </v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-menu bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  icon
                  v-bind="attrs"
                  v-on="on">
                  <v-icon>mdi-dots-vertical</v-icon>
                </v-btn>
              </template>
              <v-list>
                <v-list-item
                  v-for="(action, i) in contextActions"
                  :key="i"
                  @click="action.handler(item)">
                  <v-list-item-icon>
                    <v-icon>{{action.icon}}</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>{{action.title}}</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
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
  data() {
    return {
      contextActions: [
        {
          title: 'Remove run',
          icon: 'mdi-delete',
          handler: (item) => this.$emit('onRemove', item),
        },
        {
          title: 'Add lift above',
          icon: 'mdi-table-column-plus-before',
          handler: (item) => this.$emit('onRemove', item),
        },
        {
          title: 'Add lift below',
          icon: 'mdi-table-column-plus-after',
          handler: (item) => this.$emit('onRemove', item),
        },
      ],
    };
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
  },
};
</script>
