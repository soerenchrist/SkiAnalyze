import DataService from '../../services/DataService';
import { FETCH_TRACKS, ADD_TRACK, REMOVE_TRACK } from './actions';
import {
  ADD_TRACK_ERROR,
  ADD_TRACK_STARTED,
  FETCH_TRACKS_ERROR,
  FETCH_TRACKS_STARTED,
  FETCH_TRACKS_SUCCESS,
  REMOVE_TRACK_ERROR,
  REMOVE_TRACK_STARTED,
} from './mutations';

export default {
  state: {
    loading: false,
    tracks: [],
    error: null,
  },
  actions: {
    async [FETCH_TRACKS]({ commit }) {
      commit(FETCH_TRACKS_STARTED);
      const sessionId = localStorage.getItem('userSessionId');
      try {
        const response = await DataService.getTracks(sessionId);
        if (response.userSessionId) {
          localStorage.setItem('userSessionId', response.userSessionId);
        }
        commit(FETCH_TRACKS_SUCCESS, response.tracks);
      } catch (error) {
        commit(FETCH_TRACKS_ERROR, error);
      }
    },
    async [ADD_TRACK]({ dispatch, commit }, track) {
      commit(ADD_TRACK_STARTED);
      const sessionId = localStorage.getItem('userSessionId');
      track.userSessionId = sessionId;
      try {
        const response = await DataService.createTrack(track);
        console.log(response);
        localStorage.setItem('userSessionId', response.userSessionId);
        dispatch(FETCH_TRACKS);
      } catch (error) {
        commit(ADD_TRACK_ERROR, error);
      }
    },
    async [REMOVE_TRACK]({ dispatch, commit }, trackId) {
      commit(REMOVE_TRACK_STARTED);
      const userSessionId = localStorage.getItem('userSessionId');
      const track = {
        id: trackId,
        userSessionId,
      };
      try {
        await DataService.removeTrack(track);
        dispatch(FETCH_TRACKS);
      } catch (error) {
        commit(REMOVE_TRACK_ERROR, error);
      }
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
  },
  getters: {
    tracks(state) {
      return state.tracks;
    },
  },
};
