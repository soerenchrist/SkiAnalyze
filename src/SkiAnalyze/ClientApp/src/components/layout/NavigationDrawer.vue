<template>
  <v-navigation-drawer app v-model="drawer">
    <v-sheet color="grey lighten-4" class="pa-4">
      <v-avatar class="mb-4" color="grey darken-1" size="64"></v-avatar>

      <div>john@vuetifyjs.com</div>
    </v-sheet>
    <v-divider />

    <v-list>
      <v-list-item
          v-for="link in links"
          :key="link.icon"
          link
          :to="link.link"
        >
        <v-list-item-icon>
          <v-icon>{{ link.icon }}</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>{{ link.name }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
import { SET_NAVIGATION_DRAWER } from '../../store/mutations';

export default {
  data: () => ({
    drawer: null,
    links: [
      {
        icon: 'mdi-home',
        name: 'Dashboard',
        link: '/',
      },
      {
        icon: 'mdi-ski',
        name: 'Tracks',
        link: '/tracks',
      },
      {
        icon: 'mdi-map-marker-radius',
        name: 'Ski areas',
        link: '/skiareas',
      },
    ],
  }),
  watch: {
    drawer() {
      if (this.drawer !== this.storeDrawer) {
        this.$store.commit(SET_NAVIGATION_DRAWER, this.drawer);
      }
    },
    storeDrawer() {
      if (this.drawer !== this.storeDrawer) {
        this.drawer = this.storeDrawer;
      }
    },
  },
  computed: {
    storeDrawer() {
      return this.$store.getters.navigationDrawer;
    },
  },
};
</script>
