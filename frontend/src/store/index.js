import Vue from 'vue';
import Vuex from 'vuex';
import PistesModule from './pistes';
import GondolasModule from './gondolas';
import MapModule from './map';
import TracksModule from './tracks';
import AnalyzeModule from './analyze';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    piste: PistesModule,
    gondola: GondolasModule,
    map: MapModule,
    tracks: TracksModule,
    analyze: AnalyzeModule,
  },
  getters: {
  },
});
