<template>
  <v-app>
    <v-app-bar
      app
      color="indigo"
      dark
    >
      <div class="d-flex align-center">
        <v-img
          alt="Logo"
          class="shrink mr-2"
          contain
          src="@/assets/logo.png"
          transition="scale-transition"
          width="200"
        />
      </div>
    </v-app-bar>
    <v-card>
      <v-content  class="pt-5">
        <router-view></router-view>
      </v-content>
    </v-card>
    <BottomBar />
  </v-app>
</template>

<script>
import { BottomBar } from "@/components/bottom";

export default {
  name: 'App',


  
  components: {
    BottomBar
  },

  data: () => ({
    //
  }),
};
</script>
