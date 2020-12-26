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
        :src="logo"
        transition="scale-transition"
        width="200"
      />
    </div>
  </v-app-bar>
</template>

<script>
import settings from "@/settings.js";
export default {
    props: {
    },
    data: () => ({
    }),
    computed: {
      logo() {
        return settings.IMAGE_LOGO;
      }
    },
    mounted() {
    },
    methods: {
    }
}
</script>
