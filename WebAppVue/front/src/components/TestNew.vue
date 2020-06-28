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
                return this.$store.getters["test/getName"];
            },
            description() {
                return this.$store.getters["test/getDescription"];
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
            this.$store.dispatch("test/createItem");
        },
        methods: {
            onEdit(value) {
                this.value.name = value.name;
                this.value.description = value.description;
            },
            onSave() {
                this.$store.commit("test/setName", this.value.name);
                this.$store.commit("test/setDescription", this.value.description);

                this.$store.dispatch("test/saveItem");
                this.$router.push("/test/list");
            }
        }
    }
</script>
