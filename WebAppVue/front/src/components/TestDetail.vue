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
                    <v-text-field
                        label="name"
                        placeholder="名称"
                        v-model="name"
                        outlined
                    ></v-text-field>
                </v-col>
                <v-col>
                    <v-textarea
                        label="description"
                        placeholder="説明"
                        v-model="description"
                        outlined
                    ></v-textarea>
                </v-col>
                <v-col>
                    <v-textarea
                        label="timestamp"
                        placeholder="登録日"
                        v-model="timestamp"
                        :readonly="true"
                        outlined
                    ></v-textarea>
                </v-col>
                <v-col>
                    <v-btn small color="primary" @click="onSave">Save</v-btn>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    export default {
        name: "TestDetail",

        data() {
            return {
                index: this.$route.params.id,
            };
        },
        computed: {
            name: {
                get() {
                    return this.$store.getters["test/getName"];
                },
                set(value) {
                    this.$store.commit("test/setName", value);
                }
            },
            description: {
                get() {
                    return this.$store.getters["test/getDescription"];
                },
                set(value) {
                    this.$store.commit("test/setDescription", value);
                }
            },
            timestamp: {
                get() {
                    return this.$store.getters["test/getTimeStamp"];
                },
            }
        },
        mounted() {
            this.$store.dispatch("test/loadItem", this.index);
        },
        methods: {
            onSave() {
                this.$store.dispatch("test/updateItem");
                this.$router.push("/test/list");
            }
        }
    }
</script>
