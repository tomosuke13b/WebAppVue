<template>
  <div>
    <div class="viewImage" :class="topClass">
      <div>
        <img
            v-if="image"
            :src="imageData"
            @error="onLoadError"
            :draggable="draggable"
        />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    image: {
      type: Object,
      default: () => {}
    },
    draggable: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    imageData() {
        return this.image.contentType + "," + this.image.data;
    }
  },
  data() {
    return {
      topClass: "noBackground"
    };
  },
  mounted() {
  },
  methods: {
    onLoadError() {
      this.$emit("onLoadError");
    }
  }
};
</script>

<style scoped>
.viewImage:after{
  content:"";
  display:block;
  padding-bottom:141%!important;
}
.viewImage div {
  position:absolute;
  width:100%;
  height:100%;
}
.viewImage div>img{
  position:relative;
  width:100%;
  height:100%;
  object-fit: cover;
}
/* .singlePage {
  background-color: #d9d9d9;
  height: 150px;
  width: 85px;
  cursor: pointer;
} */

.noBackground {
  cursor: pointer;
}
/* 
.selected {
  background: linear-gradient(to bottom, #3acfd5 0%, #11c3f0 100%);
  position: relative;
  cursor: pointer;
} */
</style>
