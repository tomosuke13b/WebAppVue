<template>
    <v-card class="mx-auto" width="320">
        <v-img :src="imageData" aspect-ratio="2.75"></v-img>
        <v-card-title primary-title>
            <div>
                <h3 class="headline pink--text text--accent-2">{{item.name}}</h3>
                <div>{{item.description}}</div>
            </div>
        </v-card-title>
        <v-card-actions>
            <v-btn fab dark color="indigo" @click="onEdit"  >
                <v-icon>fa-edit</v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
    export default {
        name: "ItemCard",

        props: {
            item: {
                type: Object,
                default: null
            },
        },

        data() {
            return {
                imageData: ""
            };
        },
        computed: {
        },
        mounted() {
            this.loadImage();
        },
        methods: {
            loadImage() {
                setTimeout(
                    () => {
                        let isImageLoaded = this.$store.getters["list/getIsImageLoaded"](this.item.id);
                        if(!isImageLoaded) {
                            this.loadImage();
                            return;
                        }
                        this.imageData = this.getImage();
                    }, 100
                );
            },
            getImage() {
                let image = this.$store.getters["list/getImage"](this.item.id);
                if(image === undefined) {
                    let blankImage = this.$store.getters["list/getBlankImage"](this.item.id);
                    return blankImage;
                }
                if(!image.contentType) return "";
                return image.contentType + "," + image.data;
            },
            onEdit() {
                this.$emit("onEdit", this.item.id);
            },
            onView() {
                this.$emit("onView", this.item.id);
            }
        },
        watch: {
        },
    }
</script>
