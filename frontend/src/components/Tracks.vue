<template>
  <v-card>
    <v-card-title>
      Tracks
    </v-card-title>
    <v-card-text>
      <v-progress-circular v-if="loading" />
      <div v-else-if="tracks.length === 0">
        Start adding tracks
      </div>
      <v-list-item
        v-else
        :class="isSelected(track) ? 'grey lighten-2' : ''"
        v-for="track in tracks" :key="track.name">
        <v-list-item-content>
          <v-list-item-title>
            {{track.name}}
          </v-list-item-title>
        </v-list-item-content>
        <v-list-item-action>
          <v-btn icon>
            <v-icon color="grey" @click="() => onRemove(track)">mdi-delete</v-icon>
          </v-btn>
        </v-list-item-action>
      </v-list-item>
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="onAdd">Add track</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    tracks: Array,
    loading: Boolean,
    selectedTrack: Object,
  },
  methods: {
    onAdd() {
      this.$emit('addTrack');
    },
    onRemove(track) {
      this.$emit('removeTrack', track);
    },
    isSelected(track) {
      return this.selectedTrack && this.selectedTrack.id === track.id;
    },
  },
};
</script>
