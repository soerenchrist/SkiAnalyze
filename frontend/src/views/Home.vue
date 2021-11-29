<template>
<div class="pa-4">
  <v-row>
    <v-col
      :sm="12"
      :md="6"
      :lg="4"
      :xl="3">
      <tracks-container />
    </v-col>
    <v-col>
      <v-row>
        <v-col
          :sm="12"
          :md="6">
          <map-tile />
        </v-col>
      </v-row>
    </v-col>
  </v-row>
  <add-track-dialog-container />
</div>
</template>

<script>
import AddTrackDialogContainer from '../components/container/dialogs/AddTrackDialogContainer.vue';
import TracksContainer from '../components/container/TracksContainer.vue';
import MapTile from '../components/tiles/MapTile.vue';

export default {
  components: { TracksContainer, MapTile, AddTrackDialogContainer },
};
</script>
