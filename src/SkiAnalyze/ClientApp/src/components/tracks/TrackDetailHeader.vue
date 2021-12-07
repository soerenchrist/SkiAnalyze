<template>
<div class="d-flex justify-space-between">
  <div>
    <div class="text-h4">{{track.name}}</div>
    <div class="text-subtitle-1">
      {{formatDate(track.date)}} @ {{formatTime(track.start)}} in {{track.skiArea.name}}
    </div>
  </div>
  <div>
    <v-menu class="float-right">
      <template v-slot:activator="{on, attrs}">
        <v-btn icon v-bind="attrs" v-on="on">
          <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item @click="onDeleteClick">
          <v-list-item-icon>
            <v-icon>mdi-delete</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Delete</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-menu>
  </div>
</div>
</template>

<script>
export default {
  props: {
    track: Object,
  },
  methods: {
    formatDate(date) {
      return new Date(date).toLocaleDateString('en-US');
    },
    formatTime(date) {
      return new Date(date).toTimeString().split(' ')[0];
    },
    onDeleteClick() {
      this.$emit('delete');
    },
  },
};
</script>
