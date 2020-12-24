<template>
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
</template>

<script>

    export default {
        name: "Header",

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
