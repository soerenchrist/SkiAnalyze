import DataService from '../../services/DataService';
import { GET_PREVIEW, START_ANALYSIS } from '../analyze/actions';
import { GET_PREVIEW_SUCCESS } from '../mutations';
import {
  FETCH_TRACKS,
  ADD_TRACK,
  REMOVE_TRACK,
  SELECT_TRACK,
} from './actions';
import {
  ADD_TRACK_ERROR,
  ADD_TRACK_STARTED,
  DISPLAY_ADD_TRACK_DIALOG,
  FETCH_TRACKS_ERROR,
  FETCH_TRACKS_STARTED,
  FETCH_TRACKS_SUCCESS,
  REMOVE_TRACK_ERROR,
  REMOVE_TRACK_STARTED,
  SET_SELECTED_TRACK,
} from './mutations';

export default {
  state: {
    loading: false,
    tracks: [],
    error: null,
    showAddTrackDialog: false,
    selectedTrack: null,
  },
  actions: {
    async [FETCH_TRACKS]({ commit, dispatch }) {
      commit(FETCH_TRACKS_STARTED);
      try {
        const tracks = await DataService.getTracks();
        commit(FETCH_TRACKS_SUCCESS, tracks);
        if (tracks && tracks.length > 0) {
          dispatch(SELECT_TRACK, tracks[0]);
        }
        if (!tracks || tracks.length === 0) {
          dispatch(SELECT_TRACK, null);
        }
      } catch (error) {
        commit(FETCH_TRACKS_ERROR, error);
      }
    },
    async [ADD_TRACK]({ dispatch, commit }, track) {
      commit(ADD_TRACK_STARTED);
      try {
        const createdTrack = await DataService.createTrack(track);
        dispatch(FETCH_TRACKS);
        dispatch(START_ANALYSIS, createdTrack.id);
      } catch (error) {
        commit(ADD_TRACK_ERROR, error);
      }
    },
    async [REMOVE_TRACK]({ dispatch, commit }, trackId) {
      commit(REMOVE_TRACK_STARTED);
      try {
        await DataService.removeTrack(trackId);
        dispatch(FETCH_TRACKS);
      } catch (error) {
        commit(REMOVE_TRACK_ERROR, error);
      }
    },
    [SELECT_TRACK]({ dispatch, commit }, track) {
      if (track !== null) {
        dispatch(GET_PREVIEW, track.id);
      } else {
        commit(GET_PREVIEW_SUCCESS, null);
      }
      commit(SET_SELECTED_TRACK, track);
    },
  },
  mutations: {
    [FETCH_TRACKS_STARTED](state) {
      state.loading = true;
      state.tracks = [];
      state.error = null;
    },
    [FETCH_TRACKS_ERROR](state, error) {
      state.loading = false;
      state.tracks = [];
      state.error = error;
    },
    [FETCH_TRACKS_SUCCESS](state, tracks) {
      state.loading = false;
      tracks.forEach((x) => { x.visible = true; });
      state.tracks = tracks;
      state.error = null;
    },
    [ADD_TRACK_STARTED](state) {
      state.loading = true;
    },
    [ADD_TRACK_ERROR](state, error) {
      state.loading = false;
      state.error = error;
    },
    [REMOVE_TRACK_STARTED](state) {
      state.loading = true;
    },
    [REMOVE_TRACK_ERROR](state, error) {
      state.loading = false;
      state.error = error;
    },
    [DISPLAY_ADD_TRACK_DIALOG](state, value) {
      state.showAddTrackDialog = value;
    },
    [SET_SELECTED_TRACK](state, track) {
      state.selectedTrack = track;
    },
  },
  getters: {
    tracks(state) {
      return state.tracks;
    },
    selectedTrack(state) {
      return state.selectedTrack;
    },
    showAddTrackDialog(state) {
      return state.showAddTrackDialog;
    },
  },
};
