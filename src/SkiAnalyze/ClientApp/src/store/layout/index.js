import { SET_NAVIGATION_DRAWER } from './mutations';

export default {
  state: {
    navigationDrawer: null,
  },
  mutations: {
    [SET_NAVIGATION_DRAWER](state, drawer) {
      state.navigationDrawer = drawer;
    },
  },
  getters: {
    navigationDrawer(state) {
      return state.navigationDrawer;
    },
  },
};
