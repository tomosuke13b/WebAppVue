<template>
    <v-container>
        <v-btn class="mx-2" fixed right fab dark style="bottom: 80px" color="indigo" @click="onSave" :disabled="isView || !isSave">
            <v-icon dark>fa-save</v-icon>
        </v-btn>
        <v-btn class="mx-2" fixed right fab dark style="bottom: 160px" color="indigo" @click="onDelete" :disabled="isView">
            <v-icon dark>fa-trash</v-icon>
        </v-btn>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        編集・参照
                    </h1>
                </v-col>
                <v-col align="center" justify="center">
                    <ViewImageCarousel :images="images" @onImageClick="onImageClick" />
                </v-col>
                <v-col>
                    <Editor 
                        :value="value"
                        :isView="isView"
                        @onEdit="onEdit"
                    />
                </v-col>
                <v-col>
                    <v-text-field
                        label="登録日"
                        v-model="timestamp"
                        :readonly="true"
                    ></v-text-field>
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
        id: {
            default: -1,
            type: Number
        },
        isView: {
            default: false,
            type: Boolean
        },
    },
    data() {
        return {
            value: {
                name : "",
                description : "",
            },
        };
    },
    computed: {
        images() {
            return this.$store.getters["item/images"];
        },
        name() {
            return this.$store.getters["item/name"];
        },
        description() {
            return this.$store.getters["item/description"];
        },
        timestamp: {
            get() {
                return this.$store.getters["item/timeStamp"];
            },
        },
        isSave() {
            return this.value.name;
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
        this.$store.dispatch("item/loadItem", this.id);
    },
    methods: {
        onImageClick() {

        },
        onEdit(value) {
            this.value.name = value.name;
            this.value.description = value.description;
        },
        async onSave() {
            this.$store.commit("item/setName", this.value.name);
            this.$store.commit("item/setDescription", this.value.description);

            await this.$store.dispatch("item/updateItem", this.id);
            this.$router.push( { name: "list" } );
        },
        async onDelete() {
            await this.$store.dispatch("item/deleteItem", this.id);
            this.$router.push( { name: "list" } );
        }
    }
}
</script>
