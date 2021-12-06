<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark>
      <v-app-bar-nav-icon @click="goHome">
        <v-icon>mdi-home</v-icon>
      </v-app-bar-nav-icon>
      <v-toolbar-title>
        SkiAnalyze
      </v-toolbar-title>
    </v-app-bar>
    <navigation-drawer />
    <v-main>
      <v-container fluid>
        <router-view/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import NavigationDrawer from './components/layout/NavigationDrawer.vue';

export default {
  components: { NavigationDrawer },
  name: 'App',
  methods: {
    goHome() {
      this.$router.push('/');
    },
  },
};

</script>
