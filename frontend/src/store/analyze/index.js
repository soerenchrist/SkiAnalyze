import DataService from '../../services/DataService';
import GeoHelper from '../../services/GeoHelper';
import { SELECT_RUN, START_ANALYSIS } from './actions';
import {
  ANALYSIS_ERROR,
  ANALYSIS_STARTED,
  ANALYSIS_SUCCESS,
  SET_SELECTED_RUN,
} from './mutations';
import { SET_SELECTED_GONDOLA } from '../gondolas/mutations';
import {
  SET_MAP_BOUNDS,
} from '../mutations';

export default {
  state: {
    loading: false,
    result: null,
    error: null,
    selectedRun: null,
  },
  actions: {
    async [START_ANALYSIS]({ commit }) {
      commit(ANALYSIS_STARTED);
      const userSessionId = localStorage.getItem('userSessionId');
      try {
        const response = await DataService.startAnalysis(userSessionId);
        commit(ANALYSIS_SUCCESS, response);
      } catch (ex) {
        commit(ANALYSIS_ERROR, ex);
      }
    },
    [SELECT_RUN]({ commit }, run) {
      commit(SET_SELECTED_RUN, run);
      commit(SET_SELECTED_GONDOLA, null);
      if (run !== null) {
        commit(SET_MAP_BOUNDS, GeoHelper.getBounds(run.coordinates));
      }
    },
  },
  mutations: {
    [ANALYSIS_STARTED](state) {
      state.loading = true;
    },
    [ANALYSIS_SUCCESS](state, result) {
      state.loading = false;
      state.result = result;
    },
    [ANALYSIS_ERROR](state, error) {
      state.loading = false;
      state.error = error;
    },
    [SET_SELECTED_RUN](state, run) {
      state.selectedRun = run;
    },
  },
  getters: {
    analysisResult(state) {
      return state.result;
    },
    selectedRun(state) {
      return state.selectedRun;
    },
  },
};
