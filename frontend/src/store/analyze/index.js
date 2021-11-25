import DataService from '../../services/DataService';
import { START_ANALYSIS } from './actions';
import { ANALYSIS_ERROR, ANALYSIS_STARTED, ANALYSIS_SUCCESS } from './mutations';

export default {
  state: {
    loading: false,
    result: null,
    error: null,
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
  },
  getters: {
    analysisResult(state) {
      return state.result;
    },
  },
};
