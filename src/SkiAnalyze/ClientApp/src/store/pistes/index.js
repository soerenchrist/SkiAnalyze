import { FETCH_PISTES, SELECT_PISTE } from './actions';
import DataService from '../../services/DataService';
import {
  FETCH_PISTES_ERROR,
  FETCH_PISTES_STARTED,
  FETCH_PISTES_SUCCESS,
  SET_DIFFICULTY_FILTER,
  SET_SELECTED_PISTE,
} from './mutations';
import { SET_MAP_BOUNDS } from '../mutations';
import GeoHelper from '../../services/GeoHelper';

const filterPistes = (pistes, index) => {
  if (index === 0) return pistes;
  if (index === 1) return pistes.filter((x) => x.difficulty <= 1);
  if (index === 2) return pistes.filter((x) => x.difficulty === 2);
  if (index === 3) return pistes.filter((x) => x.difficulty === 3);
  return [];
};

export default {
  state: {
    pistesCount: 0,
    difficultyFilter: 0,
    selectedPiste: null,
    pistes: [],
    loading: false,
    error: '',
  },
  mutations: {
    [FETCH_PISTES_STARTED](state) {
      state.pistes = [];
      state.loading = true;
      state.pistesCount = 0;
    },
    [FETCH_PISTES_SUCCESS](state, response) {
      state.pistesCount = response.count;
      state.pistes = response.pistes;
      state.loading = false;
    },
    [FETCH_PISTES_ERROR](state, error) {
      state.pistesCount = 0;
      state.pistes = [];
      state.loading = false;
      state.error = error;
    },
    [SET_DIFFICULTY_FILTER](state, index) {
      state.difficultyFilter = index;
    },
    [SET_SELECTED_PISTE](state, piste) {
      state.selectedPiste = piste;
    },
  },
  actions: {
    async [FETCH_PISTES]({ commit }, bounds) {
      commit(FETCH_PISTES_STARTED);
      try {
        const response = await DataService.getPistes(bounds);
        commit(FETCH_PISTES_SUCCESS, response);
      } catch (ex) {
        commit(FETCH_PISTES_ERROR, ex);
      }
    },
    async [SELECT_PISTE]({ commit }, piste) {
      commit(SET_SELECTED_PISTE, piste);
      commit(SET_MAP_BOUNDS, GeoHelper.getBounds(piste.coordinates));
    },
  },
  getters: {
    filteredPistes(state) {
      return filterPistes(state.pistes, state.difficultyFilter);
    },
    pistes(state) {
      return state.pistes;
    },
    selectedPiste(state) {
      return state.selectedPiste;
    },
  },
};
