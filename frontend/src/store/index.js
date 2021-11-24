import Vue from 'vue';
import Vuex from 'vuex';
import PistesModule from './pistes';
import GondolasModule from './gondolas';

import {
  ADD_TRACK,
  REMOVE_TRACK,
  SET_DISPLAY_ADD_TRACK_DIALOG,
  SET_SELECTED_GONDOLA,
  SET_SELECTED_PISTE,
} from './mutations';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    tracks: [],
    showAddTrackDialog: false,
    filteredPists: [],
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
    [SET_DISPLAY_ADD_TRACK_DIALOG](state, value) {
      state.showAddTrackDialog = value;
    },
    [ADD_TRACK](state, track) {
      state.tracks.push(track);
    },
    [REMOVE_TRACK](state, track) {
      state.tracks = state.tracks.filter((x) => x !== track);
    },
  },
  actions: {
  },
  modules: {
    piste: PistesModule,
    gondola: GondolasModule,
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
