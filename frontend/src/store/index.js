import Vue from 'vue';
import Vuex from 'vuex';
import { FETCH_GONDOLAS, FETCH_PISTES } from './actions';
import DataService from '../services/DataService';
import {
  SET_GONDOLAS,
  SET_PISTES,
  SET_SELECTED_GONDOLA,
  SET_SELECTED_PISTE,
} from './mutations';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    gondolaCount: 0,
    gondolas: [],
    pisteCount: 0,
    pistes: [],
    selectedGondola: null,
    selectedPiste: null,
  },
  mutations: {
    [SET_GONDOLAS](state, response) {
      state.gondolaCount = response.count;
      state.gondolas = response.gondolas;
    },
    [SET_PISTES](state, response) {
      state.pisteCount = response.pisteCount;
      state.pistes = response.pistes;
    },
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
    async [FETCH_GONDOLAS]({ commit }) {
      const response = await DataService.getGondolas();
      commit(SET_GONDOLAS, response);
    },
    async [FETCH_PISTES]({ commit }) {
      const response = await DataService.getPistes();
      commit(SET_PISTES, response);
    },
  },
  modules: {
  },
});
