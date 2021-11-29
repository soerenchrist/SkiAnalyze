import { FETCH_GONDOLAS, SELECT_GONDOLA } from './actions';
import DataService from '../../services/DataService';
import {
  FETCH_GONDOLAS_ERROR,
  FETCH_GONDOLAS_STARTED,
  FETCH_GONDOLAS_SUCCESS,
  SET_SELECTED_GONDOLA,
} from './mutations';
import { SET_MAP_BOUNDS, SET_SELECTED_RUN } from '../mutations';
import GeoHelper from '../../services/GeoHelper';

export default {
  state: {
    gondolaCount: 0,
    gondolas: [],
    loading: false,
    error: '',
    selectedGondola: null,
  },
  mutations: {
    [FETCH_GONDOLAS_STARTED](state) {
      state.gondolas = [];
      state.loading = true;
      state.gondolaCount = 0;
    },
    [FETCH_GONDOLAS_SUCCESS](state, response) {
      state.gondolaCount = response.count;
      state.gondolas = response.gondolas;
      state.gondolasLoading = false;
    },
    [FETCH_GONDOLAS_ERROR](state, error) {
      state.gondolaCount = 0;
      state.gondolas = [];
      state.gondolasLoading = false;
      state.error = error;
    },
    [SET_SELECTED_GONDOLA](state, gondola) {
      state.selectedGondola = gondola;
    },
  },
  actions: {
    async [FETCH_GONDOLAS]({ commit }) {
      commit(FETCH_GONDOLAS_STARTED);
      try {
        const response = await DataService.getGondolas();
        commit(FETCH_GONDOLAS_SUCCESS, response);
      } catch (ex) {
        commit(FETCH_GONDOLAS_ERROR, ex);
      }
    },
    [SELECT_GONDOLA]({ commit }, gondola) {
      commit(SET_SELECTED_GONDOLA, gondola);
      commit(SET_SELECTED_RUN, null);
      if (gondola !== null) {
        commit(SET_MAP_BOUNDS, GeoHelper.getBounds(gondola.coordinates));
      }
    },
  },
  getters: {
    selectedGondola(state) {
      return state.selectedGondola;
    },
    gondolas(state) {
      return state.gondolas;
    },
  },
};
