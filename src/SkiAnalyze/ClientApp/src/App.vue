<template>
  <v-app>
    <application-bar />
    <navigation-drawer />
    <v-main>
      <v-container fluid>
        <router-view/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import ApplicationBar from './components/layout/ApplicationBar.vue';
import NavigationDrawer from './components/layout/NavigationDrawer.vue';

export default {
  components: { NavigationDrawer, ApplicationBar },
  name: 'App',
};

</script>
