<template>
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
  <gondola-details-container />
  <piste-details-container />

  <add-track-dialog-container />
</v-row>
</template>

<script>
import AddTrackDialogContainer from '../components/container/dialogs/AddTrackDialogContainer.vue';
import GondolaDetailsContainer from '../components/container/GondolaDetailsContainer.vue';
import PisteDetailsContainer from '../components/container/PisteDetailsContainer.vue';
import TabContainer from '../components/TabContainer.vue';
import Map from '../components/map/Map.vue';

import {
  FETCH_GONDOLAS, FETCH_PISTES,
} from '../store/actions';
import {
  SET_SELECTED_GONDOLA,
  SET_SELECTED_PISTE,
} from '../store/mutations';

export default {
  name: 'Home',
  components: {
    Map,
    GondolaDetailsContainer,
    PisteDetailsContainer,
    AddTrackDialogContainer,
    TabContainer,
  },
  data: () => ({
    zoom: 13,
    center: [46.966709, 11.007390],
  }),
  computed: {
    gondolas() {
      return this.$store.state.gondolas;
    },
    pistes() {
      return this.$store.state.pistes;
    },
  },
  methods: {
    onGondolaSelected(gondola) {
      this.$store.commit(SET_SELECTED_GONDOLA, gondola);
    },
    onPisteSelected(piste) {
      this.$store.commit(SET_SELECTED_PISTE, piste);
    },
  },
  mounted() {
    this.$store.dispatch(FETCH_PISTES);
    this.$store.dispatch(FETCH_GONDOLAS);
  },
};
</script>
