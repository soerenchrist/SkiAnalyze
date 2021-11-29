<template>
<div>
  <v-row>
    <tab-container />
    <v-col>
      <Map
        :center="center"
        :zoom="zoom"
        :gondolas="gondolas"
        :pistes="pistes"
        @gondolaClicked="onGondolaSelected"
        @pisteClicked="onPisteSelected" />
    </v-col>

  </v-row>
  <v-row>
    <v-col>
      <height-profile-container />
    </v-col>
  </v-row>
  <add-track-dialog-container />
  <gondola-detail-dialog />
</div>
</template>

<script>
import AddTrackDialogContainer from '../components/container/dialogs/AddTrackDialogContainer.vue';
import TabContainer from '../components/TabContainer.vue';
import HeightProfileContainer from '../components/container/charts/HeightProfileContainer.vue';
import Map from '../components/map/Map.vue';

import {
  FETCH_GONDOLAS, FETCH_PISTES, SELECT_GONDOLA, SELECT_PISTE,
} from '../store/actions';
import GondolaDetailDialog from '../components/dialogs/GondolaDetailDialog.vue';

export default {
  name: 'Home',
  components: {
    Map,
    // GondolaDetailsContainer,
    // PisteDetailsContainer,
    AddTrackDialogContainer,
    HeightProfileContainer,
    TabContainer,
    GondolaDetailDialog,
  },
  data: () => ({
    zoom: 13,
    center: [46.97448992512977, 10.324968962190015],
  }),
  computed: {
    gondolas() {
      return this.$store.getters.gondolas;
    },
    pistes() {
      return this.$store.getters.filteredPistes;
    },
  },
  methods: {
    onGondolaSelected(gondola) {
      this.$store.dispatch(SELECT_GONDOLA, gondola);
    },
    onPisteSelected(piste) {
      this.$store.dispatch(SELECT_PISTE, piste);
    },
  },
  mounted() {
    this.$store.dispatch(FETCH_PISTES);
    this.$store.dispatch(FETCH_GONDOLAS);
  },
};
</script>
