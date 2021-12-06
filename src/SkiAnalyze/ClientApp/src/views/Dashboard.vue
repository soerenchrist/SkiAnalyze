<template>
  <div>
    <v-row>
      <v-col>
        <last-tracks />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import LastTracks from '../components/dashboard/LastTracks.vue';

export default {
  components: { LastTracks },
};
</script>
