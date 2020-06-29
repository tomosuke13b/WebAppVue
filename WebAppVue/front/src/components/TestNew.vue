<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        新規
                    </h1>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/test/list">List</v-btn>
                </v-col>
                <v-col>
                    <Editor 
                        :value="value"
                        :isView="false"
                        @onEdit="onEdit"
                    />
                </v-col>
                <v-col>
                    <v-btn small color="primary" @click="onSave">Save</v-btn>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    import { Editor } from "@/components/edit";
    export default {
        name: "TestNew",
        components: { Editor },

        props: {
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
            name() {
                return this.$store.getters["item/getName"];
            },
            description() {
                return this.$store.getters["item/getDescription"];
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
            onEdit(value) {
                this.value.name = value.name;
                this.value.description = value.description;
            },
            async onSave() {
                this.$store.commit("item/setName", this.value.name);
                this.$store.commit("item/setDescription", this.value.description);

                await this.$store.dispatch("item/saveItem");
                this.$router.push( { name: "testList" } );
            }
        }
    }
</script>
