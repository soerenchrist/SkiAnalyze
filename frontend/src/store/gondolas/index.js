import { FETCH_GONDOLAS, SELECT_GONDOLA } from './actions';
import DataService from '../../services/DataService';
import {
  FETCH_GONDOLAS_ERROR,
  FETCH_GONDOLAS_STARTED,
  FETCH_GONDOLAS_SUCCESS,
  SET_SELECTED_GONDOLA,
  SET_SHOW_GONDOLA_DETAILS_DIALOG,
} from './mutations';

export default {
  state: {
    gondolaCount: 0,
    gondolas: [],
    loading: false,
    error: '',
    showGondolaDetailsDialog: false,
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
    [SET_SHOW_GONDOLA_DETAILS_DIALOG](state, value) {
      state.showGondolaDetailsDialog = value;
    },
  },
  actions: {
    async [FETCH_GONDOLAS]({ commit }, bounds) {
      commit(FETCH_GONDOLAS_STARTED);
      try {
        const response = await DataService.getGondolas(bounds);
        commit(FETCH_GONDOLAS_SUCCESS, response);
      } catch (ex) {
        commit(FETCH_GONDOLAS_ERROR, ex);
      }
    },
    [SELECT_GONDOLA]({ commit }, gondola) {
      commit(SET_SELECTED_GONDOLA, gondola);
      commit(SET_SHOW_GONDOLA_DETAILS_DIALOG, true);
    },
  },
  getters: {
    selectedGondola(state) {
      return state.selectedGondola;
    },
    gondolas(state) {
      return state.gondolas;
    },
    showGondolaDetailsDialog(state) {
      return state.showGondolaDetailsDialog;
    },
  },
};
