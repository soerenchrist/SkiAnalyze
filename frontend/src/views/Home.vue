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
    <!--<gondola-details-container />-->
    <!--<piste-details-container />-->

  </v-row>
  <add-track-dialog-container />
</div>
</template>

<script>
import AddTrackDialogContainer from '../components/container/dialogs/AddTrackDialogContainer.vue';
// import GondolaDetailsContainer from '../components/container/GondolaDetailsContainer.vue';
// import PisteDetailsContainer from '../components/container/PisteDetailsContainer.vue';
import TabContainer from '../components/TabContainer.vue';
import Map from '../components/map/Map.vue';

import {
  FETCH_GONDOLAS, FETCH_PISTES, SELECT_GONDOLA, SELECT_PISTE,
} from '../store/actions';

export default {
  name: 'Home',
  components: {
    Map,
    // GondolaDetailsContainer,
    // PisteDetailsContainer,
    AddTrackDialogContainer,
    TabContainer,
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
