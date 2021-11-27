import { TOGGLE_EXPAND_DETAILS } from './mutations';

export default {
  state: {
    detailsExpanded: false,
  },
  mutations: {
    [TOGGLE_EXPAND_DETAILS](state) {
      state.detailsExpanded = !state.detailsExpanded;
    },
  },
  getters: {
    detailsExpanded(state) {
      return state.detailsExpanded;
    },
  },
};
