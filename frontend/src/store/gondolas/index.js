import { FETCH_GONDOLAS } from './actions';
import DataService from '../../services/DataService';
import {
  FETCH_GONDOLAS_ERROR,
  FETCH_GONDOLAS_STARTED,
  FETCH_GONDOLAS_SUCCESS,
} from './mutations';

export default {
  state: {
    gondolaCount: 0,
    gondolas: [],
    loading: false,
    error: '',
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
  },
};
