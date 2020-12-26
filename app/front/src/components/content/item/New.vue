<template>
    <v-container>
        <v-btn class="mx-2" fixed right fab dark style="bottom: 80px" color="indigo" @click="onSave" :disabled="!isSave">
            <v-icon dark>fa-save</v-icon>
        </v-btn>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        新規
                    </h1>
                </v-col>
                <v-col align="center" justify="center">
                    <ViewImageCarousel :images="images" @onImageClick="onImageClick" />
                </v-col>
                <v-col>
                    <v-btn
                        small
                        color="primary"
                        @click="inputFiles"
                    >
                        ファイルを選択
                    </v-btn>
                    <input
                        type="file"
                        accept="image/png,image/jpeg, image/jpg"
                        @change="uploadImage($event)"
                        style="display: none;"
                        ref="input"
                        :multiple="true"
                    />
                </v-col>
                <v-col>
                    <Editor 
                        :value="value"
                        :isView="false"
                        @onEdit="onEdit"
                    />
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { Editor, ViewImageCarousel } from "./common";
export default {
    components: { Editor, ViewImageCarousel },

    props: {
    },
    data() {
        return {
            value: {
                name : "",
                description : "",
            },
            MAX_IMAGE_FILE_SIZE: 50000000,
        };
    },
    computed: {
        images() {
            return this.$store.getters["item/images"];
        },
        isImage() {
            return this.$store.getters["item/isImage"];
        },
        isSave() {
            return this.isImage && this.value.name;
        },
        name() {
            return this.$store.getters["item/name"];
        },
        description() {
            return this.$store.getters["item/description"];
        },
    },
    watch: {
        name(value) {
            this.value = {
                name : value,
                description : this.value.description,
            }
        },
        description(value) {
            this.value = {
                name : this.value.name,
                description : value,
            }
        },
    },
    mounted() {
        this.$store.dispatch("item/createItem");
    },
    methods: {
        inputFiles() {
            this.$refs.input.click();
        },
        uploadImage(event) {
            let target = event.target;
            let files = target.files;
            if (!this.validationImages(files))  return;
            Array.from(files).forEach(file => {
                this.readFileToImage(file);
            });
        },
        validationImages(files) {
            var validats = [];
            Array.from(files).forEach(file => {
                validats.push(this.validationImage(file));
            });
            return validats.find(validat => validat);
        },
        validationImage(file) {
            if (file.type !== "image/jpeg" && file.type !== "image/png") return false;
            if (file.size > this.MAX_IMAGE_FILE_SIZE) return false;
            return true;
        },
        readFileToImage(file) {
            var func = (imageSource) => {
                let contentType = imageSource[0];
                let data = imageSource[1];
                this.$store.commit("item/addImage", { contentType: contentType, data: data });
            };
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                // func(reader.result.split(',')[1]);
                func(reader.result.split(','));
            };
        },
        onEdit(value) {
            this.value.name = value.name;
            this.value.description = value.description;
        },
        onImageClick() {

        },
        async onSave() {
            this.$store.commit("item/setName", this.value.name);
            this.$store.commit("item/setDescription", this.value.description);

            await this.$store.dispatch("item/saveImage");
            
            await this.$store.dispatch("item/saveItem");
            this.$router.push( { name: "list" } );
        }
    }
}
</script>
