import { FETCH_PISTES } from './actions';
import DataService from '../../services/DataService';
import {
  FETCH_PISTES_ERROR,
  FETCH_PISTES_STARTED,
  FETCH_PISTES_SUCCESS,
  SET_DIFFICULTY_FILTER,
} from './mutations';

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
  },
  actions: {
    async [FETCH_PISTES]({ commit }) {
      commit(FETCH_PISTES_STARTED);
      try {
        const response = await DataService.getPistes();
        commit(FETCH_PISTES_SUCCESS, response);
      } catch (ex) {
        commit(FETCH_PISTES_ERROR, ex);
      }
    },
  },
  getters: {
    filteredPistes(state) {
      return filterPistes(state.pistes, state.difficultyFilter);
    },
  },
};
