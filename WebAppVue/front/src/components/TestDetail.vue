<template>
    <v-container>
        <v-btn class="mx-2" absolute left dark color="indigo" to="/test/list">
            <v-icon dark left>mdi-arrow-left</v-icon>Back
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
                <v-col>
                    <v-btn small color="primary" @click="onSave" :disabled="isView">Save</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary" @click="onDelete" :disabled="isView">Delete</v-btn>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    import { Editor } from "@/components/edit";
    import { ViewImageCarousel } from "@/components/image";
    export default {
        name: "TestDetail",
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
                return this.$store.getters["item/getImages"];
            },
            name() {
                return this.$store.getters["item/getName"];
            },
            description() {
                return this.$store.getters["item/getDescription"];
            },
            timestamp: {
                get() {
                    return this.$store.getters["item/getTimeStamp"];
                },
            }
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
                this.$router.push( { name: "testList" } );
            },
            async onDelete() {
                await this.$store.dispatch("item/deleteItem", this.id);
                this.$router.push( { name: "testList" } );
            }
        }
    }
</script>
