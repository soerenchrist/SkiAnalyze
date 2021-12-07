<template>
  <v-app-bar
    app
    dark>
    <v-app-bar-nav-icon @click="toggleDrawer">
      <v-icon>mdi-menu</v-icon>
    </v-app-bar-nav-icon>
  </v-app-bar>
</template>

<script>
import { SET_NAVIGATION_DRAWER } from '../../store/mutations';

export default {
  computed: {
    drawer() {
      return this.$store.getters.navigationDrawer;
    },
  },
  methods: {
    toggleDrawer() {
      this.$store.commit(SET_NAVIGATION_DRAWER, !this.drawer);
    },
  },
};
</script>
