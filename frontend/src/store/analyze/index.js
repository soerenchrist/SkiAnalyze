import DataService from '../../services/DataService';
import GeoHelper from '../../services/GeoHelper';
import {
  GET_ANALYSIS_RESULT,
  GET_PREVIEW,
  SELECT_RUN,
  START_ANALYSIS,
} from './actions';
import {
  GET_ANALYSIS_RESULT_ERROR,
  GET_ANALYSIS_RESULT_STARTED,
  GET_ANALYSIS_RESULT_SUCCESS,
  GET_PREVIEW_ERROR,
  GET_PREVIEW_STARTED,
  GET_PREVIEW_SUCCESS,
  SET_SELECTED_RUN,
} from './mutations';
import { SET_MAP_BOUNDS } from '../mutations';
import { FETCH_GONDOLAS, FETCH_TRACKS, FETCH_PISTES } from '../actions';

const sleep = (milliseconds) => new Promise((resolve) => setTimeout(resolve, milliseconds));

export default {
  state: {
    loading: false,
    status: {},
    previewLoading: false,
    error: null,
    selectedRun: null,
    preview: null,
    result: null,
  },
  actions: {
    async [START_ANALYSIS]({ dispatch }, trackId) {
      try {
        await DataService.startAnalysis(trackId);

        let status = null;
        let finished = false;

        while (!finished) {
          await sleep(2000);
          status = await DataService.getAnalysisStatus(trackId);
          if (status.isFinished) {
            finished = true;
          }
        }

        dispatch(FETCH_TRACKS);
      } catch (ex) {
        console.error(ex);
      }
    },
    async [GET_ANALYSIS_RESULT]({ commit, dispatch }, trackId) {
      commit(GET_ANALYSIS_RESULT_STARTED);
      try {
        const result = await DataService.getAnalysisResult(trackId);
        if (result.bounds) {
          dispatch(FETCH_GONDOLAS, result.bounds);
          dispatch(FETCH_PISTES, result.bounds);
          commit(SET_MAP_BOUNDS, result.bounds);
        }
        commit(GET_ANALYSIS_RESULT_SUCCESS, result);
      } catch (ex) {
        commit(GET_ANALYSIS_RESULT_ERROR, ex);
      }
    },
    [SELECT_RUN]({ commit }, run) {
      commit(SET_SELECTED_RUN, run);
      if (run !== null) {
        if (run.gondola) {
          commit(SET_MAP_BOUNDS, GeoHelper.getBounds(run.gondola.coordinates));
        } else {
          commit(SET_MAP_BOUNDS, GeoHelper.getBounds(run.coordinates));
        }
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
    [GET_ANALYSIS_RESULT_STARTED](state) {
      state.loading = true;
    },
    [GET_ANALYSIS_RESULT_SUCCESS](state, result) {
      state.loading = false;
      state.result = result;
    },
    [GET_ANALYSIS_RESULT_ERROR](state, err) {
      state.loading = false;
      state.error = err;
    },
  },
  getters: {
    analysisResult(state) {
      return state.result;
    },
    preview(state) {
      return state.preview;
    },
    selectedRun(state) {
      return state.selectedRun;
    },
  },
};
