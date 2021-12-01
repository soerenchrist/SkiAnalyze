import { SET_MAP_BOUNDS, SET_MAP_CENTER } from './mutations';

export default {
  state: {
    bounds: null,
    center: null,
  },
  actions: {

  },
  mutations: {
    [SET_MAP_BOUNDS](state, value) {
      state.bounds = value;
    },
    [SET_MAP_CENTER](state, value) {
      state.center = value;
    },
  },
  getters: {
    center(state) {
      return state.center;
    },
    bounds(state) {
      return state.bounds;
    },
  },
};
