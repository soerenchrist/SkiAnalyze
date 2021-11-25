<template>
  <v-app>
    <v-app-bar
      app
      color="secondary"
      dark
    >
      <v-spacer />
      <start-analysis-button />
    </v-app-bar>

    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
import StartAnalysisButton from './components/other/StartAnalysisButton.vue';

export default {
  name: 'App',
  components: {
    StartAnalysisButton,
  },
  data: () => ({
    //
  }),
};

</script>
