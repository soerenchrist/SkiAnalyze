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
</div>
</template>

<script>
import TracksContainer from '../components/container/TracksContainer.vue';
import MapTile from '../components/tiles/MapTile.vue';

export default {
  components: { TracksContainer, MapTile },
};
</script>
