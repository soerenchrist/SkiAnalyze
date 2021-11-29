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

    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',
  methods: {
    goHome() {
      this.$router.push('/');
    },
  },
};

</script>
