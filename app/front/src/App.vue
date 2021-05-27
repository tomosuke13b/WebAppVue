<template>
  <v-app>
    <Header />
    <v-content class="main">
      <router-view></router-view>
    </v-content>
    <Bottom />
  </v-app>
</template>

<script>
import { Header, Bottom } from "@/components";

export default {
  name: 'App',

  components: {
    Header,
    Bottom
  },

  data: () => ({
    //
  }),
};
</script>
<style scoped>
.main {
  margin-bottom: 50px;
}
</style>