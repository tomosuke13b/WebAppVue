<template>
  <v-app>
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
