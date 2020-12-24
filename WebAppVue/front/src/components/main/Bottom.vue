<template>
  <v-bottom-navigation
    style="bottom:0px"
    fixed
    horizontal
  >
    <v-btn value="home" to="/">
      <span>Home</span>
      <v-icon>fa-home</v-icon>
    </v-btn>

    <v-btn value="list" to="/test/list">
      <span>List</span>
      <v-icon>fa-list</v-icon>
    </v-btn>

    <v-btn value="info"  to="/test/info">
      <span>Info</span>
      <v-icon>fa-exclamation-circle</v-icon>
    </v-btn>
  </v-bottom-navigation>
</template>

<script>

    export default {
        name: "Bottom",

        props: {
        },
        data: () => ({
        }),
        computed: {
        },
        mounted() {
        },
        methods: {
        }
    }
</script>
