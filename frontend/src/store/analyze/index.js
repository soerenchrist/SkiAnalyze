import DataService from '../../services/DataService';
import GeoHelper from '../../services/GeoHelper';
import { GET_PREVIEW, SELECT_RUN, START_ANALYSIS } from './actions';
import {
  ANALYSIS_ERROR,
  ANALYSIS_STARTED,
  ANALYSIS_SUCCESS,
  GET_PREVIEW_ERROR,
  GET_PREVIEW_STARTED,
  GET_PREVIEW_SUCCESS,
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
    previewLoading: false,
    error: null,
    selectedRun: null,
    preview: null,
  },
  actions: {
    async [START_ANALYSIS]({ commit }, trackId) {
      commit(ANALYSIS_STARTED);
      try {
        const response = await DataService.startAnalysis(trackId);
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
    async [GET_PREVIEW]({ commit }, trackId) {
      commit(GET_PREVIEW_STARTED);
      try {
        const response = await DataService.getPreview(trackId);
        commit(GET_PREVIEW_SUCCESS, response);
      } catch (ex) {
        commit(GET_PREVIEW_ERROR, ex);
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
    [GET_PREVIEW_STARTED](state) {
      state.previewLoading = true;
    },
    [GET_PREVIEW_SUCCESS](state, preview) {
      state.preview = preview;
      state.previewLoading = false;
    },
    [GET_PREVIEW_ERROR](state) {
      state.previewLoading = false;
    },
  },
  getters: {
    analysisResult(state) {
      return state.result;
    },
    preview(state) {
      console.log(state.preview);
      return state.preview;
    },
    selectedRun(state) {
      return state.selectedRun;
    },
  },
};
