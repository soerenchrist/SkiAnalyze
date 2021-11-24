<template>
<v-row>
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
</v-row>
</template>

<script>
import GondolaDetailsContainer from '../components/container/GondolaDetailsContainer.vue';
import PisteDetailsContainer from '../components/container/PisteDetailsContainer.vue';
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
