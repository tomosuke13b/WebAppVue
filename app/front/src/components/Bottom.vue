<template>
  <v-bottom-navigation
    style="bottom:0px"
    fixed
    horizontal
  >
    <v-btn value="home" @click="router('')">
      <span>Home</span>
      <v-icon>fa-home</v-icon>
    </v-btn>

    <v-btn value="list" @click="router('list')">
      <span>List</span>
      <v-icon>fa-list</v-icon>
    </v-btn>

    <v-btn value="info" @click="router('info')">
      <span>Info</span>
      <v-icon>fa-exclamation-circle</v-icon>
    </v-btn>
  </v-bottom-navigation>
</template>

<script>
export default {
    props: {
    },
    data: () => ({
    }),
    computed: {
    },
    mounted() {
    },
    methods: {
      router(value) {
        switch(value) {
          case "list":
            this.$router.push( { name: "list" } );
            return;
          case "info":
            this.$router.push( { name: "info" } );
            return;
          case "":
          default:
            this.$router.push( { name: "home" } );
            return;
        }
      }
    }
}
</script>
