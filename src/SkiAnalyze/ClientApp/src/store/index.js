import Vue from 'vue';
import Vuex from 'vuex';
import LayoutModule from './layout';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    layout: LayoutModule,
  },
  getters: {
  },
});
