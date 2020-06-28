<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        編集・参照
                    </h1>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/test/list">List</v-btn>
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
    export default {
        name: "TestDetail",
        components: { Editor },
        data() {
            return {
                id: this.$route.params.id,
                value: {
                    name : "",
                    description : "",
                },
            };
        },
        computed: {
            isView() {
                return this.$store.getters["test/getIsView"];
            },
            name() {
                return this.$store.getters["test/getName"];
            },
            description() {
                return this.$store.getters["test/getDescription"];
            },
            timestamp: {
                get() {
                    return this.$store.getters["test/getTimeStamp"];
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
            this.$store.dispatch("test/loadItem", this.id);
        },
        methods: {
            onEdit(value) {
                this.value.name = value.name;
                this.value.description = value.description;
            },
            onSave() {
                this.$store.commit("test/setName", this.value.name);
                this.$store.commit("test/setDescription", this.value.description);

                this.$store.dispatch("test/updateItem", this.id);
                this.$router.push("/test/list");
            },
            onDelete() {
                this.$store.dispatch("test/deleteItem", this.id);
                this.$router.push("/test/list");
            }
        }
    }
</script>
