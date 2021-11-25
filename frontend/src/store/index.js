import Vue from 'vue';
import Vuex from 'vuex';
import PistesModule from './pistes';
import GondolasModule from './gondolas';
import MapModule from './map';
import TracksModule from './tracks';

import {
  SET_SELECTED_GONDOLA,
  SET_SELECTED_PISTE,
} from './mutations';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    selectedGondola: null,
    selectedPiste: null,
  },
  mutations: {
    [SET_SELECTED_GONDOLA](state, gondola) {
      state.selectedGondola = gondola;
      state.selectedPiste = null;
    },
    [SET_SELECTED_PISTE](state, piste) {
      state.selectedPiste = piste;
      state.selectedGondola = null;
    },
  },
  actions: {
  },
  modules: {
    piste: PistesModule,
    gondola: GondolasModule,
    map: MapModule,
    tracks: TracksModule,
  },
  getters: {
    gondolas(state) {
      return state.gondola.gondolas;
    },
    pistes(state) {
      return state.piste.pistes;
    },
  },
});
