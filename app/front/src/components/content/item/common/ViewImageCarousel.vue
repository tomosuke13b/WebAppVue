<template>
    <v-carousel hide-delimiters height="500" v-model="currentIndex">
        <v-carousel-item v-for="(image, imageIndex) in images" :key="imageIndex">
            <div class="bookeditpagewrapper">
                <ViewImage
                    height="200px"
                    :image="image"
                    :used="true"
                    @click.native="onImageClick"
                />
            </div>
        </v-carousel-item>
    </v-carousel>
</template>

<script>
import { ViewImage } from "./image";
export default {
    components: { ViewImage },
    props: {
        images: {
            type: Array,
            default: function() {
                return [];
            }
        }
    },
    computed: {
    },
    data() {
        return {
            currentIndex: 0,
        };
    },
    mounted() {
    },
    methods: {
        onImageClick() {
            this.$emit("onImageClick");
        },
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
